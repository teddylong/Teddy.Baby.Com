var page = new WebPage(),
    address, output, size;


address = phantom.args[0];
width = 900; height = 500;
output = phantom.args[1];


page.viewportSize = { width: width, height: height };
if (phantom.args.length === 3 && phantom.args[1].substr(-4) === ".pdf") {
    size = phantom.args[2].split('*');
    page.paperSize = size.length === 2 ? { width: size[0], height: size[1], border: '0px' }
                                         : { format: phantom.args[2], orientation: 'portrait', border: '1cm' };
}
page.open(address, function (status) {
    if (status !== 'success') {
        console.log('Unable to load the address!');
    } else {
        window.setTimeout(function () {
            page.render(output);
            phantom.exit();
        }, 900);
    }
});