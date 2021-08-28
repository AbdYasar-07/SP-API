using Microsoft.SharePoint.Client; 
using SP_API.Model;
using SP_API.Response;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Web;
using PnP.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Linq;

namespace SP_API.SpService
{
    public class SharingService : SharingServiceInterface
    {

        private readonly IOptions<MySiteConfiguration> _options;

        public SharingService(IOptions<MySiteConfiguration> options)
        {
            _options = options;
        }
        
        public ApiResponse gettingConfiguration(List<ProjectSite> projectSite)
        {
            //Getting Response to the Request and Initializing null
            ApiResponse response = null;

            //Getting Authentication
            //OfficeDevPnP.Core.AuthenticationManager authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();

            AuthenticationManager authenticationManager = new AuthenticationManager();

            using (ClientContext clientContext = authenticationManager.GetACSAppOnlyContext(_options.Value.SiteUrl,
                                                                                            _options.Value.AppId,
                                                                                            _options.Value.AppSecret))
            {
                List tableList = clientContext.Web.Lists.GetByTitle(
                                 _options.Value.ListName);


                ListItemCreationInformation listItemCreationInformation = new ListItemCreationInformation();

                // In Order to pass more than one object we've to push into this in list otherwise,
                // it should be in Single Object
                foreach (var site in projectSite)
                {
                    try
                    {
                        if (site.Title != null || site.ProjectReferenceNumber != null || site.ProjectOwner != null
                            || site.ProjectClosedDate != null || site.ProjectsDescription != null)
                        {
                            response = new ApiResponse();
                            ListItem item = tableList.AddItem(listItemCreationInformation);

                            //item["ProjectOwner"] = site.ProjectOwner

                            User user = clientContext.Web.SiteUsers.GetByEmail(site.ProjectOwner);
                            clientContext.Load(user);
                            clientContext.ExecuteQuery();


                            var ProjectOwnerValue = new FieldUserValue() { LookupId = user.Id };
                            var ProjectOwnerValues = new[] { ProjectOwnerValue };
                            item["ProjectOwner"] = ProjectOwnerValues;
                            item["Title"] = site.Title;
                            item["ProjectReferenceNumber"] = site.ProjectReferenceNumber;
                            item["ProjectCloseDate"] = site.ProjectClosedDate;
                            item["ProjectDescription"] = site.ProjectsDescription;

                            if (site.Title == null || site.ProjectOwner == null || site.ProjectReferenceNumber == null || site.ProjectClosedDate == null ||
                              site.ProjectsDescription == null)
                            {
                                var errorMessage = "Values might be null...please check and insert correctly";

                                List ErrorTableList = clientContext.Web.Lists.GetByTitle(
                                     _options.Value.ErrorListName);

                                item["Title"] = site.ToString();
                                item["Exception"] = errorMessage.ToString();

                                response.Message = "Check the HubSiteErrorDetails";
                                response.IsInserted = true;
                                response.HttpStatus = StatusCodes.Status202Accepted;

                                item.Update();
                                clientContext.ExecuteQuery();

                            }

                            item.Update();
                            clientContext.ExecuteQuery();

                            response.Message = "Record Inserted Successfully";
                            response.IsInserted = true;
                            response.HttpStatus = StatusCodes.Status200OK;
                        }
                    }
                    catch (Exception exp)
                    {
                        response = new ApiResponse();

                        List ErrorTableList = clientContext.Web.Lists.GetByTitle(
                                 _options.Value.ErrorListName);

                        ListItem item = ErrorTableList.AddItem(listItemCreationInformation);

                        var  strSiteDetails = "";
                        strSiteDetails += "Title : " + projectSite[0].Title + " ;";
                        strSiteDetails += "ProjectClosedDate : " + projectSite[0].ProjectClosedDate + ";";
                        strSiteDetails += "ProjectOwner : " + projectSite[0].ProjectOwner + ";";
                        strSiteDetails += "ProjectReferenceNumber : " + projectSite[0].ProjectReferenceNumber + ";";
                        strSiteDetails += "ProjectsDescription : " + projectSite[0].ProjectsDescription + ";"; 


                        item["JsonDetails"] = strSiteDetails.ToString();

                        item["Exception"] = exp.Message;

                        response.Message = "Values might be null....please check and insert correctly";
                        response.IsInserted = false;
                        response.HttpStatus = StatusCodes.Status400BadRequest;

                        item.Update();
                        clientContext.ExecuteQuery();
                    }
                }
            }
            return response;
        }
    }
}

