using IntegracijskiModul.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegracijskiModul.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private static IList<Notification> _ntf = new List<Notification>();

        [HttpGet("[action]")]
        public ActionResult<Notification> GetAll()
        {
            return Ok(_ntf);
        }

        [HttpGet("{id}")]
        public ActionResult<Notification> GetOne(int id)
        {
            var n = _ntf.FirstOrDefault(x => x.Id == id);
            return Ok(n);
        }

        [HttpPost]
        public ActionResult<Notification> PostOne([FromBody] Notification n)
        {
            // provjere valid + postoji

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_ntf.Any())
            {
                var nextId = _ntf.Max(x => x.Id) + 1;
                n.Id = nextId;
            }

            else
            {
                n.Id = 1;
            }

            // dodaj

            _ntf.Add(n);

            return Ok(n);
        }
        // post sa actionom

        //[HttpPost("[action]")]
        //public ActionResult Form([FromForm] FormData data)
        //{
        //    return Redirect("/notification-form.html");
        //}

        [HttpPut("{id}")]
        public ActionResult<Notification> PutOne(int id, [FromBody] Notification n)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ntf = _ntf.FirstOrDefault(x => x.Id == id);

            if (ntf != null)
            {
                ntf.Id = id;
                ntf.Reciever = n.Reciever;
                ntf.Subject = n.Subject;
                ntf.EmailBody = n.EmailBody;
            }
            else
            {
                return NotFound($"Ne postoji id:{id}");
            }

            return Ok(ntf);
        }

        [HttpDelete("{id}")]
        public ActionResult<Notification> DeleteOne(int id)
        {
            // ima li

            var ntf = _ntf.FirstOrDefault(x => x.Id == id);

            if (ntf == null)
            {
                return NotFound($"Ne postoji id:{id}");
            }
            _ntf.Remove(ntf);

            return Ok(ntf);
        }

    }
}
