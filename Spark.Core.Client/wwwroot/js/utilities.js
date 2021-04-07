function initializeInactivityTimer(dotnetHelper) {
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 60000);
    }

    function logout() {
        dotnetHelper.invokeMethodAsync("Logout");
    }
}