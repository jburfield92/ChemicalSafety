<%@ Page Title="Universities" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Universities.aspx.cs" Inherits="CollegeEventManager.Universities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- CREATE MODAL -->
    <div class="modal fade" id="CreateUniversityModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title"><asp:Label ID="Label1" runat="server" Text="New University"/></h4>
                    <asp:HiddenField ID="ModalDetailsUniversityId" runat="server" />
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:Label ID="ModalCreateUniversityTitleLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateUniversityTitleTextBox" Text="University Title:" />
                        <div class="col-md-8">
                            <asp:TextBox ID="ModalCreateUniversityTitleTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label ID="ModalCreateUniversityDescriptionLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateUniversityDescriptionTextBox" Text="University Description:" />
                        <div class="col-md-8">
                            <asp:TextBox ID="ModalCreateUniversityDescriptionTextBox" runat="server" CssClass="form-control" MaxLength="255" TextMode="MultiLine" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label ID="ModalCreateUniversityAddressLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateUniversityLocationAddressTextBox" Text="University Address:" />
                        <div class="col-md-8">
                            <asp:TextBox ID="ModalCreateUniversityLocationAddressTextBox" runat="server" CssClass="form-control" MaxLength="64" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Label ID="ModalCreateUniversityZipCodeLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-4" AssociatedControlID="ModalCreateUniversityLocationZipCodeTextBox" Text="Zip Code:" />
                        <div class="col-md-2">
                            <asp:TextBox ID="ModalCreateUniversityLocationZipCodeTextBox" runat="server" CssClass="form-control" MaxLength="5" />
                        </div>
                        <asp:Label ID="ModalCreateUniversityStateLabel" runat="server" CssClass="control-label col-md-2" AssociatedControlID="ModalCreateUniversityLocationStateDdl" Text="State:" />
                        <div class="col-md-2">
                            <asp:DropDownList ID="ModalCreateUniversityLocationStateDdl" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label ID="ModalCreateUniversityPictureLabel" runat="server" CssClass="control-label col-md-4" AssociatedControlID="ModalCreateUniversityPictureUploader" Text="University Picture:"/>
                        <div class="col-md-8">
                            <asp:FileUpload ID="ModalCreateUniversityPictureUploader" runat="server"/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button ID="CloseCreateBtn" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:button ID="SaveCreateBtn" runat="server" CssClass="btn btn-primary" Text="Save University" OnClick="SaveCreateBtn_Click"/>
                </div>
            </div>
        </div>
    </div>
    <!-- END CREATE MODAL -->

    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <div  class="page-header">
                    <h1>Universities <asp:Button ID="CreateUniversityBtn" runat="server" Text="Create New University" CssClass="btn btn-success"/></h1>
                </div>
                <asp:Label ID="OutputText" runat="server" CssClass="control-label text-danger" />
                <asp:Repeater runat="server" ID="RptUniversities" OnItemDataBound="RptUniversities_ItemDataBound">
                    <ItemTemplate>
                       <div class="panel panel-default">
                            <div class="panel-heading">
                                <asp:Label runat="server" ID="UnivesrityHeading" Text='<%# Eval("Name") %>' />
                                <asp:HiddenField runat="server" ID="UniversityIDField" Value='<%# Eval("UniversityId") %>' />
                            </div>
                            <div class="panel-body">
                                <div class="media">
                                    <div class="media-left">
                                        <asp:Image runat="server" CssClass="media-object img-rounded pull-left" ImageUrl='<%# CollegeEventManager.Universities.GetImage(Eval("Image")) %>' style="width: 128px; height: 128px;"/>
                                    </div>
                                    <div class="media-body">
                                        <asp:Label runat="server" ID="UniversityHeader" CssClass="control-label" AssociatedControlID="UniversityLbl" Text="University Email" />
                                        <asp:Label runat="server" ID="UniversityLbl" CssClass="control-label" Text='<%# Eval("UniversityEmail") %>' />
                                        <br />
                                        <asp:Label runat="server" ID="DescriptionHeader" CssClass="control-label" AssociatedControlID="DescriptionLbl" Text="University Description" />
                                        <asp:Label runat="server" ID="DescriptionLbl" CssClass="control-label" Text='<%# Eval("Description") %>' />
                                        <br />
                                        <asp:Label runat="server" ID="DateHeader" CssClass="control-label" AssociatedControlID="DateLbl" Text="University Created Date" />
                                        <asp:Label runat="server" ID="DateLbl" CssClass="control-label" Text='<%# Eval("CreatedDate") %>'/>
                                        <br />
                                        <asp:Label runat="server" ID="LocationHeader" CssClass="control-label" AssociatedControlID="DateLbl" Text="University Location" />
                                        <asp:Label runat="server" ID="LocationLbl" CssClass="control-label" Text='<%# Eval("Location") %>'/>
                                    </div>
                                </div>
                                <br />
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            <asp:Button runat="server" ID="DetailsBtn" CssClass="btn btn-primary" Text="Details" data-toggle="modal" data-target="#myModal" OnClick="DetailsBtn_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate> 
                    <FooterTemplate>
                        <asp:Label ID="EmptyLbl" runat="server" Text="Oooops... no Universities were found!" Visible="false"/>
                    </FooterTemplate>
                </asp:Repeater>
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
                    <h4><asp:Label runat="server" ID="SearchLbl" Text="Search:" CssClass="control-label" AssociatedControlID="SearchBox"/></h4>
                    <asp:TextBox ID="SearchBox" runat="server" CssClass="form-control"/>
                    <br />
                    <asp:Button ID="SearchButton" runat="server" CssClass="btn btn-success" Text="Search"/>
                    <asp:Button ID="ResetButton" runat="server" CssClass="btn btn-primary" Text="Reset"/>
                </div>
                <div>
                    <h4><asp:Label runat="server" ID="FilterLbl" Text="Filter By:" CssClass="control-label" AssociatedControlId="FilterByRadioBtnGroup"/></h4>
                    <asp:RadioButtonList runat="server" ID="FilterByRadioBtnGroup" CellPadding="2">
                        <asp:ListItem Text="None" Selected="True" Value="None"/>
                        <asp:ListItem Text="Date Created (Ascending)" Value="DateAsc"/>
                        <asp:ListItem Text="Date Created (Descending)" Value="DateDesc"/>
                        <asp:ListItem Text="Student Count (Ascending)" Value="CountAsc"/>
                        <asp:ListItem Text="Student Count (Descending)" Value="CountDesc"/>
                        <asp:ListItem Text="Name (Ascending)" Value="NameAsc" />
                        <asp:ListItem Text="Name (Descending" Value="NameDesc" />
                    </asp:RadioButtonList>

                    <asp:Button ID="FilterButton" runat="server" CssClass="btn btn-success" Text="Filter"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
