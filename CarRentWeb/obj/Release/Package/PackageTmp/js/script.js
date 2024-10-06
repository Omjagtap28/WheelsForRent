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
    var emailInput = document.querySelector('#loginUsername');
    var passwordInput = document.querySelector('#loginPassword');
    var radioButton1 = document.querySelector('#RadioButton1'); // Use static ID
    var radioButton2 = document.querySelector('#RadioButton2'); // Use static ID
    var errorMessage = document.querySelector('#ErrorMessage'); // Use static ID

    // Resetting error message
    if (errorMessage) {
        errorMessage.textContent = ""; // Use textContent directly
    }

    if (emailInput.value.trim() === '' || passwordInput.value.trim() === '') {
        errorMessage.textContent = "Please ensure both email and password fields are filled out.";
        return false;
    }

    if (!radioButton1.checked && !radioButton2.checked) {
        errorMessage.textContent = "Please select an appropriate user type (User/Admin)";
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
    debugger;
    console.log("Entered");
    return confirm("Are you sure you want to log out?");
}


// rentnow button hover logic 
function showLoginMessage() {
    var loginMessageContainer = document.getElementById('loginMessageContainer');
    var loginMessage = "You need to login first in order to continue.";

    console.log("Entering into");
    // Set the message and position
    loginMessageContainer.innerHTML = loginMessage;
    loginMessageContainer.style.display = 'block';

    // Adjust the position based on the mouse cursor
    document.onmousemove = function (e) {
        var x = e.pageX + 20;
        var y = e.pageY - 20;
        loginMessageContainer.style.left = x + 'px';
        loginMessageContainer.style.top = y + 'px';
    };
}

function hideLoginMessage() {
    var loginMessageContainer = document.getElementById('loginMessageContainer');
    loginMessageContainer.style.display = 'none';
    document.onmousemove = null; // Remove the onmousemove event
}



// contact.aspx
