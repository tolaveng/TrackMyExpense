window.recaptcha_render = function(dotNetRef, selector, sitekey) {
    var googleRecaptcha = document.getElementById(selector);
    if (!googleRecaptcha) {
        console.log('Google Recaptcha', selector + ' is not found');
        return;
    }
    if (typeof grecaptcha === 'undefined' || typeof grecaptcha.render === 'undefined') {
        setTimeout(window.recaptcha_render, 100, dotNetRef, selector, sitekey);
    } else {
        grecaptcha.render(googleRecaptcha, {
            'sitekey': sitekey,
            'callback': (response) => { dotNetRef.invokeMethodAsync('GoogleRecaptchaCallback', response); },
            'expired-callback': () => { dotNetRef.invokeMethodAsync('GoogleRecaptchaCallback', ''); },
            'error-callback': () => { dotNetRef.invokeMethodAsync('GoogleRecaptchaCallback', ''); }
        });
    }
}

window.recaptcha_response = function() {
    return grecaptcha.getResponse(googleRecaptcha);
}