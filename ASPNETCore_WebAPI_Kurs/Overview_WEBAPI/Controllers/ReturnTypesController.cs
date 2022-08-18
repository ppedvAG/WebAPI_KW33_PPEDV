using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overview_WEBAPI.Models;

namespace Overview_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnTypesController : ControllerBase
    {
        #region Native Datentypen

        [HttpGet]
        //https://localhost:7194/api/ReturnTypes
        public string GetHelloWorld()
        {
            return "Hello World";
        }

        //ContentResult gibt ein String als Ausgabe zurück
        //-> HttpGet -> /GetCurrentTime1 -> https://localhost:7194/GetCurrentTime1
        [HttpGet("/GetCurrentTime1")]
        public ContentResult GetCurrentTime()
        {
            return Content(DateTime.Now.ToString());
        }

        //-> HttpGet -> GetCurrentTime2 -> https://localhost:7194/api/ReturnTypes/GetCurrentTime2
        [HttpGet("GetCurrentTime2")]
        public ContentResult GetCurrentTime2()
        {
            return Content(DateTime.Now.ToString());
        }

        [HttpGet("GetComplexObject")]
        public Car GetComplexCar()
        {
            Car car = new Car();
            car.Id = 1;
            car.Brand = "VW";
            car.Model = "POLO";

            return car;
        }

        #endregion



        #region IActionResult / ActionResult

        //ActionResult -> Bei GET - Methoden
        //IActionResult -> Bei POST / PUT / DELETE - Methoden

        #region Synchrone Methoden
        //ActionResult und IActionResult können Datensätze + Http-Codes zurück geben
        [HttpGet("GetGarWith_IActionResult")]
        public IActionResult GetCarWith_IActionResult()
        {
            Car car = new Car();
            car.Id = 1;
            car.Brand = "VW";
            car.Model = "911er";

            if (car.Brand != "VW")
                return BadRequest(); // 400 Error Code

            if (car.Brand == string.Empty)
                return NotFound(); // 404

            return Ok(car); //Car-Objekt als JSON + 200er Code
        }

        [HttpGet("GetGarWith_ActionResult")]
        public ActionResult GetCarWith_ActionResult()
        {
            Car car = new Car();
            car.Id = 1;
            car.Brand = "VW";
            car.Model = "911er";

            if (car.Brand != "VW")
                return BadRequest(); // 400 Error Code

            if (car.Brand == string.Empty)
                return NotFound(); // 404

            return Ok(car); //Car-Objekt als JSON + 200er Code
        }
        #endregion

        #region Assynchrone Methoden
        //async funktioniert nur, wenn await in der Methode verwendet wird
        [HttpGet("GetGarWith_IActionResult_Async")]
        public async Task<IActionResult> GetCarWith_IActionResultAsync()
        {
            await Task.Delay(1000); // Im Normalfall würden wir via EF-Core eine Asynchrone Methode mit await verwenden
            // -> Beispiel für EF-Core: Car car = await _context.Cars.SingleAsync(c=>c.Id == id);
            Car car = new Car();
            car.Id = 1;
            car.Brand = "VW";
            car.Model = "911er";

            if (car.Brand != "VW")
                return BadRequest(); // 400 Error Code

            if (car.Brand == string.Empty)
                return NotFound(); // 404

            return Ok(car); //Car-Objekt als JSON + 200er Code
        }




        //async funktioniert nur, wenn await in der Methode verwendet wird
        [HttpGet("GetGarWith_ActionResult_Async")]
        public async Task<ActionResult> GetCarWith_ActionResultAsync()
        {
            await Task.Delay(1000); // Im Normalfall würden wir via EF-Core eine Asynchrone Methode mit await verwenden
            // -> Beispiel für EF-Core: Car car = await _context.Cars.SingleAsync(c=>c.Id == id);
            Car car = new Car();
            car.Id = 1;
            car.Brand = "VW";
            car.Model = "911er";

            if (car.Brand != "VW")
                return BadRequest(); // 400 Error Code

            if (car.Brand == string.Empty)
                return NotFound(); // 404

            return Ok(car); //Car-Objekt als JSON + 200er Code
        }
        #endregion

        #endregion
    }
}
