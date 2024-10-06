function showFormLink_Click() {
    // Redirect to profile.aspx if user is logged in, otherwise to index.aspx
    var isLoggedIn = <%= Session["Email"] != null ? "true" : "false" %>;

    if (isLoggedIn) {
        window.location.href = "profile.aspx";
    } else {
        window.location.href = "index.aspx";
    }
}