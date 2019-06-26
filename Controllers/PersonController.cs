using SampleRESTServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleRESTServer.Controllers
{
    public class PersonController : ApiController
    {
        /// <summary>
        /// Get all person
        /// </summary>
        /// <returns></returns>
        // GET: api/Person
        public ArrayList Get()
        {
            PersonPersistence pp = new PersonPersistence();
            return pp.GetPersons();
        }
        /// <summary>
        /// Get a single person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Person/5
        public Person Get(int id)
        {
            PersonPersistence pp = new PersonPersistence();
            Person p = pp.GetPerson(id);
            return p;
        }
        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {
            PersonPersistence personPersistence = new PersonPersistence();
            long id=personPersistence.savePerson(value);
            value.ID = id;
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.Created);
            httpResponseMessage.Headers.Location = new Uri(Request.RequestUri, String.Format("/api/person/{0}", id));
            return httpResponseMessage;

        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Person/5
        public HttpResponseMessage Put(int id, [FromBody]Person value)
        {
            PersonPersistence personPersistence = new PersonPersistence();
            HttpResponseMessage response;
            bool updated = personPersistence.updatePerson(id, value);
            if (updated)
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Person/5
        public HttpResponseMessage Delete(int id)
        {
            PersonPersistence personPersistence = new PersonPersistence();
            HttpResponseMessage response;
            if (personPersistence.DeletePerson(id))
            {
                 response= Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
