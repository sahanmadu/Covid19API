using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Covid19API.Models;
using Newtonsoft.Json;

namespace Covid19API.Controllers
{
    [RoutePrefix("api/citizen")]
    public class CitizensController : ApiController
    {
        private MyModelCitizens mdo = new MyModelCitizens();


        [HttpGet]
        [Route("findall")]
        public HttpResponseMessage findAll()
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(mdo.Citizens.Select(p => new
                {
                    nid = p.nid,
                    fname=p.fname,
                    email=p.email,
                    age=p.age
                    


                }).ToList()));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }


        [HttpGet]
        [Route("find/{nid}")]
        public HttpResponseMessage find(string nid)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject
                    (mdo.Citizens.Select(p => new
                    {
                        nid = p.nid,
                        fname = p.fname,
                        email = p.email,
                        age = p.age
                    }


                        ).Where(p=> p.nid == nid).FirstOrDefault()));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch 
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }


        [HttpPost]
        [Route("add")]
        public HttpResponseMessage add(Citizen citizen)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                mdo.Citizens.Add(citizen);
                mdo.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }



        [HttpDelete]
        [Route("delete/{nid}")]
        public HttpResponseMessage delete(string nid)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                mdo.Citizens.Remove(mdo.Citizens.SingleOrDefault(p => p.nid == nid));
                mdo.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }



        [HttpPut]
        [Route("update")]
        public HttpResponseMessage update(Citizen citizen)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var currentCitizen = mdo.Citizens.SingleOrDefault(p => p.nid == citizen.nid);
                currentCitizen.fname = citizen.fname;
                currentCitizen.age = citizen.age;
                currentCitizen.address = citizen.address;
                currentCitizen.lat = citizen.lat;
                currentCitizen.lang = citizen.lang;
                currentCitizen.professin = citizen.professin;
                currentCitizen.email = citizen.email;
                currentCitizen.affiliation = citizen.affiliation;
                currentCitizen.password = citizen.password;
                currentCitizen.healthStatus = citizen.healthStatus;

                mdo.SaveChanges();

                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }


    }
}
