function myFunction(message) {
    console.log("from utilities " + message);
}

function dotNetStaticInvocation() {
    DotNet.invokeMethodAsync("Calculo.Client", "GetCurrentCount")
        .then(result => {
            console.log("count from js: " + result);
        });
}

function dotNetInstanceInvocation(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}