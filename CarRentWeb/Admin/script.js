function toggleLoginForm() {
    var formContainer = document.getElementById('loginFormContainer');
    formContainer.classList.toggle('hidden'); // Toggle the 'hidden' class
}

// Event listener for the login form link
document.getElementById("showFormLink").addEventListener("click", function (e) {
    e.preventDefault(); // Prevent the default action of the link
    toggleLoginForm(); // Toggle the visibility of the form
});

// Your custom validation function
function customValidationFunction() {
    var emailInput = document.getElementById('loginUsername');
    var passwordInput = document.getElementById('loginPassword');
    var radioButton1 = document.getElementById('<%= RadioButton1.ClientID %>');
    var radioButton2 = document.getElementById('<%= RadioButton2.ClientID %>');
    var errorMessage = document.getElementById('<%= ErrorMessage.ClientID %>');

    // Resetting error message
    errorMessage.innerText = "";

    if (emailInput.value.trim() === '' || passwordInput.value.trim() === '') {
        errorMessage.innerText = "Please ensure both email and password fields are filled out.";
        return false;
    }

    if (!radioButton1.checked && !radioButton2.checked) {
        errorMessage.innerText = "Please select an appropriate user type (User/Admin)";
        return false;
    }

    return true;
}


document.getElementById("loginButton").addEventListener("click", function (e) {
    // Check your custom validation logic here
    var isValid = customValidationFunction();

    if (!isValid) {
        // If custom validation fails, prevent default action
        e.preventDefault();
        return false;
    }

    // If custom validation passes and radio button is selected, allow the form to submit
    console.log("Validation Result: true");
    return true;
});

//logout method
function confirmLogout() {
    return confirm("Are you sure you want to log out?");
}



// updateTime.js

function updateTime() {
    var now = new Date();
    document.querySelectorAll('Time').innerText = now.toLocaleString();
}


// Call the function once to update the time immediately
updateTime();
