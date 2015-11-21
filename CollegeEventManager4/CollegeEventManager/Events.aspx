<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="CollegeEventManager.Events" %>
<%@ Register  Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit.HtmlEditor"  TagPrefix="HTMLEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- DETAILS MODAL -->
            <div class="modal fade"  id="detailsModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><asp:Label ID="EventNameTextLabel" runat="server"/></h4>
                            <a href="https://twitter.com/intent/tweet" id="twitterButton" class="twitter-hashtag-button" data-text="Look at this text" runat="server">Tweet</a>
                            <div id="facebookButton" runat="server" class="fb-share-button" data-href="https://google.com" data-layout="button" style="vertical-align:top;"></div>

                            <script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+'://platform.twitter.com/widgets.js';fjs.parentNode.insertBefore(js,fjs);}}(document, 'script', 'twitter-wjs');</script>

                               <div id="fb-root"></div>
                                <script>(function(d, s, id) {
                                var js, fjs = d.getElementsByTagName(s)[0];
                                if (d.getElementById(id)) return;
                                js = d.createElement(s); js.id = id;
                                js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.5";
                                 fjs.parentNode.insertBefore(js, fjs);
                                }(document, 'script', 'facebook-jssdk'));</script>

                            <asp:HiddenField ID="ModalDetailsEventId" runat="server" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Image ID="ModalEventImage" runat="server" CssClass="img-rounded" width="150px" Height="150px" EnableViewState="true"/>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventDescriptionLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventDescriptionText" Text="Event Description:" />
                                <asp:Label ID="ModalDetailsEventDescriptionText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventDateLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventDateText" Text="Event Date:" />
                                <asp:Label ID="ModalDetailsEventDateText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventDatePublishedLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventDatePublishedText" Text="Date Published:" />
                                <asp:Label ID="ModalDetailsEventDatePublishedText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventAdminLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventAdminText" Text="Event Admin:" />
                                <asp:Label ID="ModalDetailsEventAdminText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventContactPhoneLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventContactPhoneText" Text="Contact Phone:" />
                                <asp:Label ID="ModalDetailsEventContactPhoneText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventContactEmailLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventContactEmailText" Text="Contact Email:" />
                                <asp:Label ID="ModalDetailsEventContactEmailText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventRSOLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventRSOText" Text="RSO:" />
                                <asp:Label ID="ModalDetailsEventRSOText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventAttendeeCountLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventAteendeeCountText" Text="Event Attendee Count:" />
                                <asp:Label ID="ModalDetailsEventAteendeeCountText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventTypeLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventTypeText" Text="Event Type:" />
                                <asp:Label ID="ModalDetailsEventTypeText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventCategoryLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventCategoryText" Text="Event Category:" />
                                <asp:Label ID="ModalDetailsEventCategoryText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventLocationLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsEventLocationText" Text="Event Location:" />
                                <asp:Label ID="ModalDetailsEventLocationText" runat="server" CssClass="control-label col-md-6" />
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventAttendeeListLabel" runat="server" CssClass="control-label col-md-12 text-center" AssociatedControlID="ModalDetailsEventAttendeeListBox" Text="Event Attendee List:" />
                                <asp:ListBox ID="ModalDetailsEventAttendeeListBox" runat="server" CssClass="form-control col-md-12" />
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalDetailsEventRatingLabel" runat="server" CssClass="control-label col-md-12" AssociatedControlID="ModalDetailsEventRatingControl" Text="Event Rating:"/>
                            </div>
                            <asp:UpdatePanel ID="RatingPanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <ajaxToolkit:Rating ID="ModalDetailsEventRatingControl" runat="server" CurrentRating="0" MaxRating="5" 
                                            StarCssClass="shiningstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" OnChanged="ModalDetailsEventRatingControl_Changed" AutoPostBack="true"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="ModalDetailsEventRatingCount" runat="server" CssClass="control-label col-md-12" />
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="ModalDetailsEventRatingAverage" runat="server" CssClass="control-label col-md-12" />
                                    </div>
                                    <br />
                                    <div class="row">
                                        <asp:Label ID="ModalDetailsEventCommentLabel" runat="server" CssClass="control-label col-md-12" AssociatedControlID="rptComments" Text="Event Comments:" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="CommentPanel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <asp:Repeater ID="rptComments" runat="server">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <asp:Image ID="UserImage" CssClass="img-rounded" runat="server" AlternateText="User Image" ImageUrl='<%# GetImage(DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("UserID")).Picture)%>' Height="75px" Width="75px" />
                                                                <asp:HiddenField ID="UserId" runat="server" Value='<%# Eval("UserID") %>' />
                                                                <asp:HiddenField ID="CommentId" runat="server" Value='<%# Eval("CommentID") %>' />
                                                            </div>
                                                            <div class="col-md-10">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="FirstName" CssClass="control-label" runat="server" Font-Bold="true" Text='<%# DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("UserID")).FirstName %>'/>
                                                                        <asp:Label ID="LastName" CssClass="control-label" runat="server" Font-Bold="true" Text='<%# DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("UserID")).LastName %>'/>
                                                                    </div>
                                                                    <div class="col-md-offset-6 col-md-1">
                                                                        <asp:LinkButton ID="EditButton" runat="server" CssClass="glyphicon glyphicon-edit" OnClick="EditButton_Click" AutoPostBack="true"  />
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <asp:LinkButton ID="DeleteButton" runat="server" CssClass="glyphicon glyphicon-remove" OnClick="DeleteButton_Click" AutoPostBack="true" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="PostedDate" CssClass="control-label" runat="server" Text='<%# Eval("CommentDate") %>' font-italic="true" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <asp:Label ID="Comment" CssClass="control-label col-md-12 pull-left" runat="server" Text='<%# Eval("Text") %>'/>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="EditCommentTextBox" CssClass="form-control" runat="server" Text='<%# Eval("Text") %>' Visible="false" />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Button ID="EditCommentConfirm" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="EditCommentConfirm_Click" AutoPostBack="true" Visible="false"/>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Button ID="EditCommentCancel" CssClass="btn btn-primary" runat="server" Text="Cancel" OnClick="EditCommentCancel_Click" Visible="false" AutoPostBack="true" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="CommentsMessage" CssClass="text-danger" runat="server" />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="CommentTextBox" runat="server" CssClass="form-control" MaxLength="200" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="AddCommentBtn" runat="server" CssClass="btn btn-primary" OnClick="AddCommentBtn_Click" Text="Add Comment" AutoPostBack="true" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="MaxLengthLabel" runat="server" CssClass="col-md-12 control-label" ForeColor="#999999" Text="Only 200 Characters" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="googleMap" style="width:500px;height:380px;" hidden></div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:button ID="CloseDetailsBtn" runat="server" CssClass="btn btn-default" data-dismiss="modal" autopostback="true" Text="Close" />
                            <asp:button ID="JoinBtn" runat="server" CssClass="btn btn-success" Text="Join Event" OnClick="JoinEventBtn_Click" AutoPostBack="true"/>
                            <asp:Button ID="LeaveBtn" runat="server" CssClass="btn btn-warning" Text="Leave Event" OnClick="LeaveBtn_Click" AutoPostBack="true"/>
                            <asp:button ID="EditBtn" runat="server" CssClass="btn btn-primary" Text="Edit Event" OnClick="EditBtn_Click" AutoPostBack="true"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END DETAILS MODAL -->

            <!-- EDIT MODAL -->
            <div class="modal fade" id="EditEventModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><asp:Label ID="EventNameEditLabel" runat="server" Text="Edit Event"/></h4>
                            <asp:HiddenField ID="ModalEditEventId" runat="server" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:Label ID="ModalEditEventTitleLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventTitleTextBox" Text="Event Title:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditEventTitleTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventDescriptionlabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventDescriptionTextBox" Text="Event Description:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditEventDescriptionTextBox" runat="server" CssClass="form-control" MaxLength="255" TextMode="MultiLine"/>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventDateLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventDateTextBox" Text="Event Date:" />
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <span class="input-group-addon" id="EditEventAddon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                                        <asp:TextBox ID="ModalEditEventDateTextBox" runat="server" aria-describedby="EditEventAddon" CssClass="form-control"/>
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="ModalEditEventDateTextBox"
                                        PopupButtonID="imgCalendar"/>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventTimeLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventTimeHourDdl" Text="Event Time:" />
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ModalEditEventTimeHourDdl" runat="server" CssClass="form-control">
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ModalEditEventTimeMinuteDdl" runat="server" CssClass="form-control"  >
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ModalEditEventTimeOfDayDdl" runat="server" CssClass="form-control" >
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventTypeLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventTypeDdl" Text="Event Type:" />
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ModalEditEventTypeDdl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventCategoryLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventCategoryDdl" Text="Event Category:" />
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ModalEditEventCategoryDdl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventLocationLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventAddressTextBox" Text="Event Address:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditEventAddressTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventPictureLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventPictureUploader" Text="Event Picture:"/>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="ModalEditEventPictureUploader" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventContactPhone" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventContactPhoneLbl" Text="Contact Phone:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditEventContactPhoneLbl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventContactEmail" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventContactEmailLbl" Text="Contact Email:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditEventContactEmailLbl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditEventRSOLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditEventRSODdl" Text="RSO:" />
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ModalEditEventRSODdl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button ID="CloseEditBtn" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:button ID="SaveBtn" runat="server" CssClass="btn btn-primary" Text="Save Event" OnClick="SaveBtn_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EDIT MODAL -->

            <!-- CREATE MODAL -->
            <div class="modal fade" id="CreateEventModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><asp:Label ID="ModalCreateEventTitle" runat="server" Text="New Event"/></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:Label ID="ModalCreateEventTitleLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventTitleTextBox" Text="Event Title:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateEventTitleTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventDescriptionLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventDescriptionTextBox" Text="Event Description:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateEventDescriptionTextBox" runat="server" CssClass="form-control" MaxLength="255" TextMode="MultiLine" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventDateLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventDateTextBox" Text="Event Date:" />
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <span class="input-group-addon" id="CreateEventAddon"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                                        <asp:TextBox ID="ModalCreateEventDateTextBox" runat="server" aria-describedby="CreateEventAddon" CssClass="form-control"/>
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="ModalCreateEventDateTextBox"
                                        PopupButtonID="imgCalendar" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventTimeLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventTimeHourDdl" Text="Event Time:" />
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ModalCreateEventTimeHourDdl" runat="server" CssClass="form-control">
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ModalCreateEventTimeMinuteDdl" runat="server" CssClass="form-control" >
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ModalCreateEventTimeOfDayDdl" runat="server" CssClass="form-control" >
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventTypeLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventTypeDdl" Text="Event Type:" />
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ModalCreateEventTypeDdl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventCategoryLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventCategoryDdl" Text="Event Category:" />
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ModalCreateEventCategoryDdl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventAddressLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventLocationAddressTextBox" Text="Event Address:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateEventLocationAddressTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                                    <input id ="verifyAddress" type="button" runat="server" hidden/>
                                    <input id ="verifyResult" type="button" runat="server" value="button" hidden/>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventPictureLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventPictureUploader" Text="Event Picture:"/>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="ModalCreateEventPictureUploader" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventContactPhone" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventContactPhoneLbl" Text="Contact Phone:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateEventContactPhoneLbl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventContactEmail" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventContactEmailLbl" Text="Contact Email:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateEventContactEmailLbl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateEventRSO" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateEventRSODdl" Text="RSO:" />
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ModalCreateEventRSODdl" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button ID="CloseCreateBtn" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:button ID="SaveCreateBtn" runat="server" CssClass="btn btn-primary" Text="Save Event" OnClick="SaveCreateBtn_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END CREATE MODAL -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="SaveBtn" />
                <asp:PostBackTrigger ControlID="SaveCreateBtn" />
            </Triggers>
        </asp:UpdatePanel>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <div  class="page-header">
                    <h1>College Events <asp:Button ID="CreateEventBtn" runat="server" Text="Create New Event" CssClass="btn btn-success" data-toggle="modal" data-target="#CreateEventModal"/></h1>
                </div>
                <asp:UpdatePanel ID="EventsUpdatePanel" runat="server">
                    <ContentTemplate>
                        <asp:Repeater runat="server" ID="RptEvents" OnItemDataBound="RptEvents_ItemDataBound">
                            <ItemTemplate>
                               <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <asp:Label runat="server" ID="EventHeading" Text='<%# Eval("EventName") %>' />
                                        <asp:HiddenField runat="server" ID="EventIDField" Value='<%# Eval("EventID") %>' />
                                    </div>
                                    <div class="panel-body">
                                        <div class="media">
                                            <div class="media-left">
                                                <asp:Image runat="server" CssClass="media-object img-rounded pull-left" ImageUrl='<%# GetImage(Eval("Picture")) %>' style="width: 128px; height: 128px;"/>
                                            </div>
                                            <div class="media-body">
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="DateHeader" CssClass="control-label" AssociatedControlID="DateLbl" Text="Event Date:" />
                                                    <asp:Label runat="server" ID="DateLbl" CssClass="control-label" Text='<%# Eval("EventDate") %>'/>
                                                    <br />
                                                    <asp:Label runat="server" ID="LocationHeader" CssClass="control-label" AssociatedControlID="LocationLbl" Text="Location:" />
                                                    <asp:Label runat="server" ID="LocationLbl" CssClass="control-label" Text='<%# DatabaseCommunicationMethods.Sql.GetLocationById((int)Eval("LocationID")).Address %>' />
                                                    <input id ="locStuff" type="button" value='<%# DatabaseCommunicationMethods.Sql.GetLocationById((int)Eval("LocationID")).Address %>' hidden></input>
                                                    <br />
                                                    <asp:Label runat="server" ID="DescriptionHeader" CssClass="control-label" AssociatedControlID="DescriptionLbl" Text="Description:" />
                                                    <asp:Label runat="server" ID="DescriptionLbl" CssClass="control-label" Text='<%# Eval("Description") %>' />
                                                    <br />
                                                    <asp:Label runat="server" ID="AdminHeader" CssClass="control-label" AssociatedControlID="AdminLbl" Text="Event Admin:" />
                                                    <asp:Label runat="server" ID="AdminLbl" CssClass="control-label" Text='<%# string.Format("{0} {1}", 
                                                        DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("AdminID")).FirstName, 
                                                        DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("AdminID")).LastName) %>' />
                                                    <br />
                                                    <asp:Label runat="server" ID="ContactEmailHeader" CssClass="control-label" AssociatedControlID="ContactEmailLbl" Text="Contact Email:" />
                                                    <asp:Label runat="server" ID="ContactEmailLbl" CssClass="control-label" Text='<%# Eval("ContactEmail") %>' />
                                                    <br />
                                                    <asp:Label runat="server" ID="ContactPhoneHeader" CssClass="control-label" AssociatedControlID="ContactPhoneLbl" Text="Contact Phone:" />
                                                    <asp:Label runat="server" ID="ContactPhoneLbl" CssClass="control-label" Text='<%# Eval("ContactPhone") %>' />
                                                    <br />
                                                    <asp:Label runat="server" ID="DateCreatedHeader" CssClass="control-label" AssociatedControlID="DateCreatedLbl" Text="Date Published:" />
                                                    <asp:Label runat="server" ID="DateCreatedLbl" CssClass="control-label" Text='<%# Eval("DatePublished") %>' />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" ID="RatingtHeader" CssClass="control-label" AssociatedControlID="DateCreatedLbl" Text="Event Rating:" />
                                                    <asp:Label runat="server" ID="EventRatingLbl" CssClass="control-label" />
                                                    <br />
                                                    <asp:Label runat="server" ID="AttendeeCountHeader" CssClass="control-label" AssociatedControlID="DateCreatedLbl" Text="Attendee Count:" />
                                                    <asp:Label runat="server" ID="AttendeeCountLbl" CssClass="control-label" Text='<%# DatabaseCommunicationMethods.Sql.GetAttendeesByEventID(((int)Eval("EventID"))).Count%>' />
                                                    <br />
                                                    <asp:Label runat="server" ID="RSOHeader" CssClass="control-label" AssociatedControlID="DateCreatedLbl" Text="RSO:" />
                                                    <asp:Label runat="server" ID="RSOLbl" CssClass="control-label" Text='<%# DatabaseCommunicationMethods.Sql.GetRSOById((int)Eval("RSOID")) == null ? "" : DatabaseCommunicationMethods.Sql.GetRSOById((int)Eval("RSOID")).RSOName%>' />
                                                    <br />
                                                    <asp:Label runat="server" ID="UniversityHeader" CssClass="control-label" AssociatedControlID="DateCreatedLbl" Text="University:" />
                                                    <asp:Label runat="server" ID="UniversityLbl" CssClass="control-label" Text='<%# DatabaseCommunicationMethods.Sql.GetUniversityById((int)Eval("UniversityID")).Name %>' />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3">
                                                    <asp:Button runat="server" ID="JoinEventBtn" CssClass="btn btn-success" Text="Join Event" OnClick="JoinEventBtn_Click" AutoPostBack="true"/>
                                                    <asp:Button runat="server" ID="DetailsBtn" CssClass="btn btn-primary" Text="Details" OnClick="DetailsBtn_Click" AutoPostBack="true"/>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:Button ID="PublicBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="Public" />
                                                    <asp:Button ID="PrivateBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="Private" />
                                                    <asp:Button ID="RSOBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="RSO" />
                                                    <asp:Button ID="FundraisingBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="Fundraising" />
                                                    <asp:Button ID="SocialBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="Social" />
                                                    <asp:Button ID="EdcuationalBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="Educational" />
                                                    <asp:Button ID="EntertainmentBtn" runat="server" CssClass="btn btn-default" Enabled="false" Text="Entertainment" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate> 
                            <FooterTemplate>
                                <asp:Label ID="EmptyLbl" runat="server" Text="Oooops... no events were found!" Visible="false"/>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div class="pull-left">
                    <asp:LinkButton ID="lbtnFirst" CssClass="btn btn-default" runat="server" CausesValidation="false" OnClick="lbtnFirst_Click">&laquo;</asp:LinkButton>
                    <asp:LinkButton ID="lbtnPrevious" CssClass="btn btn-default" runat="server" CausesValidation="false" OnClick="lbtnPrevious_Click">&lsaquo;</asp:LinkButton>
                </div>
                <div class="pull-left">
                    <asp:DataList ID="dlPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnPaging" CssClass="btn btn-default" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="Paging" Text='<%# Eval("PageText") %>' />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div> 
                    <asp:LinkButton ID="lbtnNext" CssClass="btn btn-default" runat="server" CausesValidation="false" OnClick="lbtnNext_Click">&rsaquo;</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" CssClass="btn btn-default" runat="server" CausesValidation="false" OnClick="lbtnLast_Click">&raquo;</asp:LinkButton>
                </div>

                <div>
                    <hr />
                    <footer>
                        <p class="text-center">&copy; <%: DateTime.Now.Year %> - College Event Manager</p>
                    </footer>
                </div>
            </div>
            <div class="col-md-3 sidebar">
                <div class="form-group">
                    <h4><asp:Label runat="server" ID="SearchLbl" Text="Search:" CssClass="control-label" AssociatedControlID="SearchBox"/></h4><h6>Press Enter to Search!</h6>
                    <asp:TextBox ID="SearchBox" runat="server" CssClass="form-control" OnTextChanged="FilterChange" />
                </div>
                <div>
                    <h5><asp:Label runat="server" ID="FilterLbl" Text="Filter By:" CssClass="control-label" AssociatedControlId="FilterByRadioBtnGroup" /></h5>
                    <asp:RadioButtonList runat="server" ID="FilterByRadioBtnGroup" CellPadding="2" OnSelectedIndexChanged="FilterChange">
                        <asp:ListItem Text="None" Selected="True" Value="None"/>
                        <asp:ListItem Text="My RSO" Value="RSO"/>
                        <asp:ListItem Text="Public Event" Value="Public"/>
                        <asp:ListItem Text="Private Event (My University)" Value="Private"/>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
