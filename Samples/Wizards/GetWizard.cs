using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Wizards.APIException;
using Button = Com.Zoho.Crm.API.Wizards.Button;
using ChartData = Com.Zoho.Crm.API.Wizards.ChartData;
using Container = Com.Zoho.Crm.API.Wizards.Container;
using Criteria = Com.Zoho.Crm.API.Wizards.Criteria;
using Node = Com.Zoho.Crm.API.Wizards.Node;
using ResponseHandler = Com.Zoho.Crm.API.Wizards.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Wizards.ResponseWrapper;
using Screen = Com.Zoho.Crm.API.Wizards.Screen;
using Segment = Com.Zoho.Crm.API.Wizards.Segment;
using Wizard = Com.Zoho.Crm.API.Wizards.Wizard;
using WizardsOperations = Com.Zoho.Crm.API.Wizards.WizardsOperations;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Wizards
{
    public class GetWizard
	{
		public static void GetWizard_1(long wizardId)
		{
			WizardsOperations wizardsOperations = new WizardsOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (WizardsOperations.GetWizardbyIDParam.LAYOUT_ID, "3477061091055");
			APIResponse<ResponseHandler> response = wizardsOperations.GetWizardById(wizardId, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (new List<int>(){ 204, 304}.Contains(response.StatusCode))
				{
					Console.WriteLine (response.StatusCode == 204 ? "No Content" : "Not Modified");
					return;
				}
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;
						List<Wizard> wizards = responseWrapper.Wizards;
						foreach (Wizard wizard in wizards)
						{
							Console.WriteLine ("Wizard CreatedTime: " + wizard.CreatedTime);
							Console.WriteLine ("Wizard ModifiedTime: " + wizard.ModifiedTime);
                            Com.Zoho.Crm.API.Modules.Modules module = wizard.Module;
							if (module != null)
							{
								Console.WriteLine ("Wizard Module ID: " + module.Id);
								Console.WriteLine ("Wizard Module apiName: " + module.APIName);
							}
							Console.WriteLine ("Wizard Name: " + wizard.Name);
							MinifiedUser modifiedBy = wizard.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Wizard Modified By Name : " + modifiedBy.Name);
								Console.WriteLine ("Wizard Modified By id : " + modifiedBy.Id);
								Console.WriteLine ("Wizard Modified By Name : " + modifiedBy.Email);
							}
							List<Com.Zoho.Crm.API.Profiles.Profile> profiles = wizard.Profiles;
							foreach (Com.Zoho.Crm.API.Profiles.Profile profile in profiles)
							{
								Console.WriteLine ("Wizard Profile  Name: " + profile.Name);
								Console.WriteLine ("Wizard Profile ID: " + profile.Id);
							}
							Console.WriteLine ("Wizard Active: " + wizard.Active);
							List<Container> containers = wizard.Containers;
							foreach (Container container in containers)
							{
								Console.WriteLine ("Wizard Container ID: " + container.Id);
                                Com.Zoho.Crm.API.Layouts.Layouts layout = container.Layout;
								if (layout != null)
								{
									Console.WriteLine ("Wizard Container Layout ID: " + layout.Id);
									Console.WriteLine ("Wizard Container Layout Name: " + layout.Name);
								}
								ChartData chartData = container.ChartData;
								if (chartData != null)
								{
									List<Node> nodes = chartData.Nodes;
									foreach (Node node in nodes)
									{
										Console.WriteLine ("Chart Data Node poistion y: " + node.PosY);
										Console.WriteLine ("Chart Data Node poistion X: " + node.PosX);
										Console.WriteLine ("Chart Data Node start node: " + node.StartNode);
										Screen nodeScreen = node.Screen;
										if (nodeScreen != null)
										{
											Console.WriteLine (" screens id: " + nodeScreen.Id);
											Console.WriteLine ("display label: " + nodeScreen.DisplayLabel);
										}
									}
									List<Com.Zoho.Crm.API.Wizards.Connection> connections = chartData.Connections;
									if (connections != null)
									{
										foreach (Com.Zoho.Crm.API.Wizards.Connection connection in connections)
										{
											Screen connectionScreen = connection.TargetScreen;
											if (connectionScreen != null)
											{
												Console.WriteLine (" screens id: " + connectionScreen.Id);
												Console.WriteLine ("display label: " + connectionScreen.DisplayLabel);
											}
											Button connectionButton = connection.SourceButton;
											if (connectionButton != null)
											{
												Console.WriteLine (" button id: " + connectionButton.Id);
												Console.WriteLine ("display label: " + connectionButton.DisplayLabel);
											}
										}
									}
									Console.WriteLine ("Chart Data Canvas width: " + chartData.CanvasWidth);
									Console.WriteLine ("Chart Data Canvas height: " + chartData.CanvasHeight);
								}
								List<Screen> screens = container.Screens;
								if (screens != null)
								{
									foreach (Screen screen in screens)
									{
										Console.WriteLine (" screens id: " + screen.Id);
										Console.WriteLine ("display label: " + screen.DisplayLabel);
										List<Segment> segments = screen.Segments;
										foreach (Segment segment in segments)
										{
											Console.WriteLine ("screens segment id: " + segment.Id);
											Console.WriteLine ("screens segment equence number: " + segment.SequenceNumber);
											Console.WriteLine ("screens segment display label: " + segment.DisplayLabel);
											Console.WriteLine ("screens segment type: " + segment.Type);
											Console.WriteLine ("screens segment column count: " + segment.ColumnCount);
											List<Com.Zoho.Crm.API.Fields.Fields> fields = segment.Fields;
											if (fields != null)
											{
												foreach (Com.Zoho.Crm.API.Fields.Fields field in fields)
												{
													Console.WriteLine ("screens segment field id: " + field.Id);
													Console.WriteLine ("screens segment field apiname: " + field.APIName);
												}
											}
											List<Button> buttons = segment.Buttons;
											if (buttons != null)
											{
												foreach (Button button in buttons)
												{
													Criteria criteria = button.Criteria;
													if (criteria != null)
													{
														PrintCriteria(criteria);
													}
													Screen targetScreen = button.TargetScreen;
													if (targetScreen != null)
													{
														Console.WriteLine (" Button targetScreen id : " + targetScreen.Id);
														Console.WriteLine (" Button targetScreen display label: " + targetScreen.DisplayLabel);
													}
													Console.WriteLine (" Button display label: " + button.DisplayLabel);
													Console.WriteLine (" Button id: " + button.Id);
													Console.WriteLine (" Button display label: " + button.DisplayLabel);
													Console.WriteLine (" Button type: " + button.Type);
													Console.WriteLine (" Button bg color: " + button.BackgroundColor);
													Console.WriteLine (" Button sequence number: " + button.SequenceNumber);
													Console.WriteLine (" Button color: " + button.Color);
													Console.WriteLine (" Button shape: " + button.Shape);
													Console.WriteLine (" Button sequence number: " + button.SequenceNumber);
												}
											}
										}
									}
								}
							}
							MinifiedUser createdBy = wizard.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Wizard Created By Name : " + createdBy.Name);
								Console.WriteLine ("Wizard Created By id : " + createdBy.Id);
								Console.WriteLine ("Wizard Created By Name : " + createdBy.Email);
							}
							Wizard parentWizard = wizard.ParentWizard;
							if (parentWizard != null)
							{
								Console.WriteLine ("Wizard Folder  id : " + parentWizard.Id);
								Console.WriteLine ("Wizard Folder  Name : " + parentWizard.Name);
							}
							Console.WriteLine ("Wizard Draft: " + wizard.Draft);
							Console.WriteLine ("Wizard ID: " + wizard.Id);
						}
					}
					else if (responseHandler is APIException)
					{
						APIException exception = (APIException) responseHandler;
						Console.WriteLine ("Status: " + exception.Status.Value);
						Console.WriteLine ("Code: " + exception.Code.Value);
						Console.WriteLine ("Details: ");
						foreach (KeyValuePair<string, object> entry in exception.Details)
						{
							Console.WriteLine (entry.Key + ": " + entry.Value);
						}
						Console.WriteLine ("Message: " + exception.Message.Value);
					}
				}
				else
				{
                    Model responseObject = response.Model;
                    Type type = responseObject.GetType();
                    Console.WriteLine("Type is : {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    Console.WriteLine("Properties (N = {0}) :", props.Length);
                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
				}
			}
		}
		private static void PrintCriteria(Criteria criteria)
		{
			if (criteria.Comparator != null)
			{
				Console.WriteLine ("CustomView Criteria Comparator: " + criteria.Comparator);
			}
			if (criteria.Field != null)
			{
				Console.WriteLine ("CustomView Criteria field name: " + criteria.Field.APIName);
			}
			if (criteria.Value != null)
			{
				Console.WriteLine ("CustomView Criteria Value: " + criteria.Value);
			}
			List<Criteria> criteriaGroup = criteria.Group;
			if (criteriaGroup != null)
			{
				foreach (Criteria criteria1 in criteriaGroup)
				{
					PrintCriteria(criteria1);
				}
			}
			if (criteria.GroupOperator != null)
			{
				Console.WriteLine ("CustomView Criteria Group Operator: " + criteria.GroupOperator);
			}
		}
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				long wizardId = 34770619497009;
                GetWizard_1(wizardId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}