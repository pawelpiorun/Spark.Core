console.log("Spark.Core.Client startup.js");

var cssId = "sparkstylescss";
if (!document.getElementById(cssId)) {
    var head = document.getElementsByTagName('head')[0];
    var link = document.createElement('link');
    link.id = cssId;
    link.rel = 'stylesheet';
    link.type = 'text/css';
    link.href = '_content/Spark.Core.Client/css/styles.css';
    link.media = 'all';
    head.appendChild(link);
}

var cssId2 = "facss";
if (!document.getElementById(cssId2)) {
    var head = document.getElementsByTagName('head')[0];
    var link = document.createElement('link');
    link.id = cssId2;
    link.rel = 'stylesheet';
    link.type = 'text/css';
    link.href = 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css';
    link.media = 'all';
    head.appendChild(link);

    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "_content/Spark.Core.Client/js/utilities.js";
    head.append(s);
}