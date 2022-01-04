// https://www.geekinsta.com/add-javascript-in-blazor-components/
// store list of what scripts we've loaded
JSLoaded = [];
window.loadJSScript = function(scriptUrl, isAsync, isDefer) {
	if (JSLoaded[scriptUrl]) return;

	if (scriptUrl.Length == 0) {
		console.error("Invalid source URL");
		return;
	}

	return new Promise(function (resolve, reject) {
		var scriptEl = document.createElement('script');
		scriptEl.src = scriptUrl;
		scriptEl.type = "text/javascript";
		scriptEl.defer = !!isDefer;
		scriptEl.async = !!isAsync;

		scriptEl.onload = function () {
			//console.log("Script " + scriptUrl + " loaded successfully");
			resolve(scriptUrl);
		}

		scriptEl.onerror = function () {
			//console.error("Failed to load  " + scriptUrl + " script");
			reject(scriptUrl);
		}

		document.body.appendChild(scriptEl);
		JSLoaded[scriptUrl] = true;
	});
}

window.redirectToPage = function(redirectUrl, second) {
	var delay = second ? second * 1000 : 0;
	setTimeout(function () {
		window.location.replace(redirectUrl);
	}, delay)
}