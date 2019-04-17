function dynamicLoadCss(url) {
    var head = document.getElementsByTagName('head')[0];
    var link = document.createElement('link');
    link.type = 'text/css';
    link.rel = 'stylesheet';
    link.href = url;
    head.appendChild(link);
}

(function () {
    var styles = ['default.css', 'simple.css'];
    var currentStyle = window.localStorage.getItem('currentStyle') || styles[0];
    var cssSelect = $('<select style="position:absolute;bottom:5px;right:5px;"></select>');
    var styleurl = './assets/theme/' + currentStyle;
    dynamicLoadCss(styleurl);

    $(cssSelect).val(currentStyle);
    for (var i = 0; i < styles.length; i++) {
        var el = $('<option></option>');
        var val = styles[i];
        $(el).val(val);
        $(el).text(val);
        if (val == currentStyle)
            $(el).attr('selected', true);
        $(cssSelect).append(el);
    }

    $(cssSelect).change(function (selStyle) {
        window.localStorage.setItem('currentStyle', $(cssSelect).val());
        window.location.reload();
    });

    $(window.document.body).append(cssSelect);
})();