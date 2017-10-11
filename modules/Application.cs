using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bogus;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using NancyService.contexts;
using NancyService.helpers;
using NancyService.models;
namespace NancyService.modules
{
    public class Application : NancyModule
    {
        static List<String> sampleClaims = new List<string>();
        
        public Application(AppDbContext db): base("/applications")
        {
            sampleClaims.Add("claim1");
            sampleClaims.Add("claim2");
//            this.RequiresAuthentication();
            
           
            Get("/", MiddlewareWrapper.intercept(this,args =>
            {
               // TODO: make sure that it's from the user\
                var analysts = db.Applications.Where(application => sampleClaims.Contains(application.BaseUserClaim)).ToList();
                return Response.AsJson(analysts);
            }));
           
            // TODO: might want to use the name or something?
            Get("/{id}", MiddlewareWrapper.intercept(this, args =>
            {
                int valueId = args.id;
                var application  = db.Applications.FirstOrDefault(value => value.ApplicationID == valueId);
                if (application == null || !application.IsActive)
                {
                    return HttpStatusCode.NotFound;
                }
                return Response.AsJson(application);
            }));
            
            Delete("{id}", MiddlewareWrapper.intercept(this,args =>
            {
                
                var identity = Context.CurrentUser;
                var userId = identity.Identity.Name; 
                int valueId = args.id;
                var analyst = db.Applications.FirstOrDefault(analy => analy.ApplicationID == valueId);
                analyst.IsActive = false;
                db.SaveChanges();
                return Response.AsJson(analyst);
            }));
            
            Post("/new", MiddlewareWrapper.intercept(this,args =>
            {
                var identity = Context.CurrentUser;
                var userID = identity.Identity.Name;
                var application = this.Bind<models.Application>();
                application.IsActive = true;
                db.Applications.Add(application);
                db.SaveChanges();
                return Response.AsJson(application);
            }));
            
//            Get("/seed", MiddlewareWrapper.intercept(this,args =>
//            {
//                var testAnalysts = new Faker<Application>()
//                    .RuleFor(u => u.DomainLoginCode, f => f.Person.UserName)
//                    .RuleFor(u => u.GHMAnalystCode, f => f.Person.UserName)
//                    .RuleFor(u => u.CreatedDate, f => f.Date.Past())
//                    .RuleFor(u => u.IsActive, f => true)
//                    .RuleFor(u => u.CreatedUser, f => f.Person.UserName);
//                var analysts = testAnalysts.Generate(20);
//                db.Analysts.AddRange(analysts);
//                db.SaveChanges();
//                return HttpStatusCode.OK;
//            }));
        }

    }
}