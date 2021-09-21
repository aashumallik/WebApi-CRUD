using AspNetWebApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApiRest.Controllers
{
    public class ListItemsController : ApiController
    {
        private static List<CustomListItem> _listItems { get; set; } = new List<CustomListItem>();
        // GET api/<controller>
        public IEnumerable<CustomListItem> Get()
        {
            return _listItems;
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var item = _listItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] CustomListItem model)
        {
            if (string.IsNullOrEmpty(model?.Text))
            {
                //Returns a Bad Request response if the list item Text property is missing or blank
                return Request.CreateResponse(HttpStatusCode.BadRequest); 
            }
            //
            var maxId = 0;
            //Calculates the next available ID
            if (_listItems.Count > 0)
            {
                maxId = _listItems.Max(x => x.Id);
            }
            model.Id = maxId + 1;
            //Assigns the ID and adds the item to the list
            _listItems.Add(model);
            //Returns the whole item (including the new id) in the response body, along with a Created status code
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var item = _listItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _listItems.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}