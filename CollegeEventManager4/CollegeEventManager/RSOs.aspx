<%@ Page Title="RSOs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RSOs.aspx.cs" Inherits="CollegeEventManager.RSOs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- DETAILS MODAL -->
            <div class="modal fade"  id="detailsModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><asp:Label ID="RSONameTextLabel" runat="server" /></h4>
                            <asp:HiddenField ID="ModalDetailsRSOId" runat="server" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Image ID="ModalRSOImage" runat="server" CssClass="img-rounded" />
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSODescriptionLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsRSODescriptionText" Text="RSO Description:" />
                                <asp:Label ID="ModalDetailsRSODescriptionText" runat="server" CssClass="control-label col-md-6" Text="No RSO Description"/>
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSOUniversityLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsRSOUniversityText" Text="RSO University:" />
                                <asp:Label ID="ModalDetailsRSOUniversityText" runat="server" CssClass="control-label col-md-6" Text="No RSO University"/>
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSOAdminLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsRSOAdminText" Text="RSO Admin:" />
                                <asp:Label ID="ModalDetailsRSOAdminText" runat="server" CssClass="control-label col-md-6" Text="No RSO Admin" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSODateLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsRSODateText" Text="RSO Created Date:" />
                                <asp:Label ID="ModalDetailsRSODateText" runat="server" CssClass="control-label col-md-6" Text="No RSO Date" />
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSOMemberCountLabel" runat="server" CssClass="control-label col-md-6" AssociatedControlID="ModalDetailsRSOMemberCountText" Text="RSO Member Count:" />
                                <asp:Label ID="ModalDetailsRSOMemberCountText" runat="server" CssClass="control-label col-md-6" Text="0" />
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSOMemberListLabel" runat="server" CssClass="control-label col-md-12 text-center" AssociatedControlID="ModalDetailsRSOMemberListBox" Text="RSO Member List:" />
                                <asp:ListBox ID="ModalDetailsRSOMemberListBox" runat="server" CssClass="form-control col-md-12" />
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalDetailsRSOEventsListLabel" runat="server" CssClass="control-label col-md-12 text-center" AssociatedControlID="ModalDetailsRSOEventsListBox" Text="RSO Events List:" />
                                <asp:ListBox ID="ModalDetailsRSOEventsListBox" runat="server" CssClass="form-control col-md-12" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button ID="CloseDetailsBtn" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:button ID="JoinBtn" runat="server" CssClass="btn btn-success" Text="Join RSO" OnClick="JoinRSOBtn_Click"/>
                            <asp:Button ID="LeaveBtn" runat="server" CssClass="btn btn-warning" Text="Leave RSO" OnClick="LeaveBtn_Click" />
                            <asp:button ID="EditBtn" runat="server" CssClass="btn btn-primary" Text="Edit RSO" OnClick="EditBtn_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END DETAILS MODAL -->

            <!-- EDIT MODAL -->
            <div class="modal fade" id="EditRSOModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><asp:Label ID="RSONameEditLabel" runat="server" Text="RSO Name"/></h4>
                            <asp:HiddenField ID="ModalEditRSOId" runat="server" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:Label ID="ModalEditRSOTitleLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditRSOTitleTextBox" Text="RSO Title:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditRSOTitleTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalEditRSODescriptionlabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditRSODescriptionTextBox" Text="RSO Description:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalEditRSODescriptionTextBox" runat="server" CssClass="form-control" MaxLength="255" TextMode="MultiLine"/>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label ID="ModalEditRSOPictureLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalEditRSOPictureUploader" Text="RSO Picture:"/>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="ModalEditRSOPictureUploader" runat="server"/>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button ID="CloseEditBtn" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:button ID="SaveBtn" runat="server" CssClass="btn btn-primary" Text="Save RSO" OnClick="SaveBtn_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EDIT MODAL -->

            <!-- CREATE MODAL -->
            <div class="modal fade" id="CreateRSOModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"><asp:Label ID="Label1" runat="server" Text="New RSO"/></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:Label ID="ModalCreateRSOTitleLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSOTitleTextBox" Text="RSO Title:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateRSOTitleTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateRSODescriptionLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSODescriptionTextBox" Text="RSO Description:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateRSODescriptionTextBox" runat="server" CssClass="form-control" MaxLength="255" TextMode="MultiLine" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateRSOUserOne" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSOUserOneTextBox" Text="Initial User 1:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateRSOUserOneTextBox" runat="server" CssClass="form-control" MaxLength="255" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateRSOUserTwo" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSOUserTwoTextBox" Text="Initial User 2:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateRSOUserTwoTextBox" runat="server" CssClass="form-control" MaxLength="255" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateRSOUserThree" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSOUserThreeTextBox" Text="Initial User 3:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateRSOUserThreeTextBox" runat="server" CssClass="form-control" MaxLength="255" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateRSOUserFour" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSOUserFourTextBox" Text="Initial User 4:" />
                                <div class="col-md-8">
                                    <asp:TextBox ID="ModalCreateRSOUserFourTextBox" runat="server" CssClass="form-control" MaxLength="255" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Label ID="ModalCreateRSOPictureLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateRSOPictureUploader" Text="RSO Picture:"/>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="ModalCreateRSOPictureUploader" runat="server" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button ID="CloseCreateBtn" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:button ID="SaveCreateBtn" runat="server" CssClass="btn btn-primary" Text="Save RSO" OnClick="SaveCreateBtn_Click" />
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
                    <h1>College RSOs <asp:Button ID="CreateRSOBtn" runat="server" Text="Create New RSO" CssClass="btn btn-success" data-toggle="modal" data-target="#CreateRSOModal"/></h1>
                </div>
                <asp:Label ID="OutputText" runat="server" CssClass="control-label text-danger" />
                <asp:UpdatePanel ID="RsoUpdatePanel" runat="server">
                    <ContentTemplate>
                        <asp:Repeater runat="server" ID="RptRSOs" OnItemDataBound="RptRSOs_ItemDataBound">
                            <ItemTemplate>
                               <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <asp:Label runat="server" ID="RSOHeading" Text='<%# Eval("RSOName") %>' />
                                        <asp:HiddenField runat="server" ID="RSOIDField" Value='<%# Eval("RSOID") %>' />
                                    </div>
                                    <div class="panel-body">
                                        <div class="media">
                                            <div class="media-left">
                                                <asp:Image runat="server" CssClass="media-object img-rounded pull-left" ImageUrl='<%# CollegeEventManager.RSOs.GetImage(Eval("Picture")) %>' style="width: 128px; height: 128px;"/>
                                            </div>
                                            <div class="media-body">
                                                <asp:Label runat="server" ID="Date" CssClass="control-label" AssociatedControlID="DateLbl" Text="Date Created:" />
                                                <asp:Label runat="server" ID="DateLbl" CssClass="control-label" Text='<%# Eval("CreatedDate") %>' />
                                                <br />
                                                <asp:Label runat="server" ID="UniversityHeader" CssClass="control-label" AssociatedControlID="UniversityLbl" Text="University:" />
                                                <asp:Label runat="server" ID="UniversityLbl" CssClass="control-label" Text='<%# DatabaseCommunicationMethods.Sql.GetUniversityById((int)Eval("UniversityID")).Name %>' />
                                                <br />
                                                <asp:Label runat="server" ID="DescriptionHeader" CssClass="control-label" AssociatedControlID="DescriptionLbl" Text="RSO Description:" />
                                                <asp:Label runat="server" ID="DescriptionLbl" CssClass="control-label" Text='<%# Eval("Description") %>' />
                                                <br />
                                                <asp:Label runat="server" ID="AdminHeader" CssClass="control-label" AssociatedControlID="AdminLbl" Text="Admin Name:" />
                                                <asp:Label runat="server" ID="AdminLbl" CssClass="control-label" Text='<%# string.Format("{0} {1}", 
                                                                DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("AdminID")).FirstName, 
                                                                DatabaseCommunicationMethods.Sql.GetUserById((Guid)Eval("AdminID")).LastName) %>' />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3">
                                                    <asp:Button runat="server" ID="JoinRSOBtn" CssClass="btn btn-success" Text="Join RSO" OnClick="JoinRSOBtn_Click" AutoPostBack="true"/>
                                                    <asp:Button runat="server" ID="DetailsBtn" CssClass="btn btn-primary" Text="Details" OnClick="DetailsBtn_Click" AutoPostBack="true"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate> 
                            <FooterTemplate>
                                <asp:Label ID="EmptyLbl" runat="server" Text="Oooops... no RSOs were found!" Visible="false"/>
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
                    <asp:TextBox ID="SearchBox" runat="server" CssClass="form-control" OnTextChanged="FilterChanged"/>
                </div>
                <div>
                    <h4><asp:Label runat="server" ID="FilterLbl" Text="Filter By:" CssClass="control-label" AssociatedControlId="FilterByRadioBtnGroup"/></h4>
                    <asp:RadioButtonList runat="server" ID="FilterByRadioBtnGroup" CellPadding="2" OnSelectedIndexChanged="FilterChanged">
                        <asp:ListItem Text="None" Selected="True" Value="None"/>
                        <asp:ListItem Text="My University" Value="University"/>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>