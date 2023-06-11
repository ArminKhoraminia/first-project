using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        #region Create Context
        //gfhfghjf
//hghghghghggg
        private ICourseService _courseService;
        public CourseApiController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [Produces("application/json")]
        [HttpGet("SearchBar")]
        public async Task<IActionResult> SearchBar()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var titleList = _courseService.SearchTitleForSearchBar(term);
                return Ok(titleList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
