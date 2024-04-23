namespace OnePiece.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using One_Piece.Service.Interfaces;
    using One_Piece.Service.Statistics;
    using System.Net;

    [Route("api/statistics")]
    [ApiController]
    public class StatisticsAPIController : ControllerBase
    {
        private readonly IMissionService missionService;

        public StatisticsAPIController(IMissionService missionService)
        {
            this.missionService = missionService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetStatisticsAsync()
        {
            try
            {
                StatisticsServiceModel serviceModel =
                    await this.missionService.GetStatisticsAsync();

                return this.Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
