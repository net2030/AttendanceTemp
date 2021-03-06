// PageMethods.js

var displayElement;

// Initializes global variables and session state.
function pageLoad() {
    alert("");
    displayElement = $get("ResultId");
    PageMethods.SetSessionValue("SessionValue", Date(),
        OnSucceeded, OnFailed);
}

// Gets the session state value.
function GetSessionValue(key) {
    PageMethods.GetSessionValue(key,
        OnSucceeded, OnFailed);
    alert("");
}

//Sets the session state value.
function SetSessionValue(key, value) {
    PageMethods.SetSessionValue(key, value,
        OnSucceeded, OnFailed);
}

// Callback function invoked on successful 
// completion of the page method.
function OnSucceeded(result, userContext, methodName) {
    if (methodName == "GetSessionValue") {
        displayElement.innerHTML = "Current session state value: " +
            result;
    }

}

// Callback function invoked on failure 
// of the page method.
function OnFailed(error, userContext, methodName) {
    if (error !== null) {
        displayElement.innerHTML = "An error occurred: " +
            error.get_message();
    }
    alert("");
}

if (typeof (Sys) !== "undefined") Sys.Application.notifyScriptLoaded();
