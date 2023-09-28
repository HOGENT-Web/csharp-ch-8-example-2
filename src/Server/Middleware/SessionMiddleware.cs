using System;
using BogusStore.Domain.Exceptions;
using BogusStore.Shared.Infrastructure;
using System.Net;
using ISession = BogusStore.Domain.Sessions.ISession;
using BogusStore.Domain.Sessions;
using System.Security.Claims;

namespace BogusStore.Server.Middleware;

public class SessionMiddleware
{
    private readonly ILogger<SessionMiddleware> logger;
    private readonly RequestDelegate next;

    public SessionMiddleware(ILogger<SessionMiddleware> logger, RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISession session)
    {
        if (httpContext.User.Identity!.IsAuthenticated)
        {
            session.UserId = httpContext.User.GetUserId();
        }

        await next(httpContext);
    }
}
