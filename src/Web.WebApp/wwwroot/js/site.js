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

CssLoaded = [];
window.loadCss = function (cssUrl) {
	if (CssLoaded[cssUrl]) return;

	if (cssUrl.Length == 0) {
		console.error("Invalid source URL");
		return;
	}

	return new Promise(function (resolve, reject) {
		var cssEl = document.createElement('link');
		cssEl.setAttribute("rel", "stylesheet");
		cssEl.setAttribute("type", "text/css");
		cssEl.setAttribute("href", cssUrl);

		cssEl.onload = function () {
			resolve(cssUrl);
		}

		cssEl.onerror = function () {
			reject(cssUrl);
		}

		document.getElementsByTagName("head")[0].appendChild(cssEl);
		CssLoaded[cssUrl] = true;
	});
}

window.redirectToPage = function(redirectUrl, second) {
	const delay = second ? second * 1000 : 0;
	setTimeout(function () {
		window.location.replace(redirectUrl);
	}, delay)
}

// Get browser time zone to set the timezone value
window.GetBrowserTimeZone = function () {
	const currentTimeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
	return currentTimeZone ? currentTimeZone : '';
}


// https://docs.microsoft.com/en-us/aspnet/core/blazor/images?view=aspnetcore-6.0#stream-images
// Test image steam
window.addDocumentImage = async (imageStream) => {
	const arrayBuffer = await imageStream.arrayBuffer();
	const blob = new Blob([arrayBuffer]);
	const url = URL.createObjectURL(blob);
	var img = document.createElement("img");
	img.src = url;
	document.body.appendChild(img);
}