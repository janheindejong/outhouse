using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Presentation.Identity;

namespace OutHouse.Server.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ApplicationBaseController : ControllerBase
    {

        protected UserContext UserContext => new(HttpContext);

        protected async Task<ActionResult<TResult>> ExecuteWithExceptionHandling<TResult>(
            Task<TResult> actionDelegate,
            IResultFactory<TResult> resultFactory)
        {
            TResult result;
            try
            {
                result = await actionDelegate;
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (SeeOtherException)
            {
                // TODO Implement location header
                return new StatusCodeResult(303);
            }

            return resultFactory.Build(result);
        }

        protected interface IResultFactory<TResult>
        {
            ActionResult<TResult> Build(TResult result);
        }

        protected class OkResultFactory<TResult> : IResultFactory<TResult>
        {
            public ActionResult<TResult> Build(TResult result)
            {
                return new OkObjectResult(result);
            }
        }

        protected class CreatedAtActionResultFactory<TResult>(
            string? actionName, ControllerBase controller)
            : IResultFactory<TResult> where TResult : IEntity
        {
            public ActionResult<TResult> Build(TResult result)
            {
                return controller.CreatedAtAction(actionName, new { id = result.Id }, result);
            }
        }
    }
}
