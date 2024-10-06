function confirmLogout() {
    console.log("Log out successfully");
    return confirm("Are you sure you want to log out?");
}



function hideConfirmationMessage(confirmationDivID) {
    setTimeout(function () {
        var confirmationDiv = document.getElementById(confirmationDivID);
        if (confirmationDiv) {
            confirmationDiv.style.display = 'none';
        }
    }, 10000); // 10 seconds
}

