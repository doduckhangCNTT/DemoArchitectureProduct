﻿using System.Transactions;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.Behaviors;
public sealed class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2

    public TransactionPipelineBehavior(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    /// <summary>
    /// Hàm xử lí Transaction của Pipleline.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        #region ============== SQL-SERVER-STRATEGY-1 ==============

        //// Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //// https://learn.microsoft.com/ef/core/miscellaneous/connection-resiliency
        var strategy = _context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            {
                var response = await next();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return response;
            }
        });
        #endregion ============== SQL-SERVER-STRATEGY-1 ==============

        #region ============== SQL-SERVER-STRATEGY-2 ==============

        //IMPORTANT: passing "TransactionScopeAsyncFlowOption.Enabled" to the TransactionScope constructor. This is necessary to be able to use it with async/await.
        //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //{
        //    var response = await next(); // next() là để nhảy sang hàm Handler()
        //    await _unitOfWork.SaveChangesAsync(cancellationToken);
        //    transaction.Complete();
        //    await _unitOfWork.DisposeAsync();
        //    return response;
        //}
        #endregion ============== SQL-SERVER-STRATEGY-2 ==============

    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}
