
// https://foliotek.github.io/Croppie/
var profileImageCroppie = null;
window.renderProfileImageCropper = function (imageUrl) {
    const profileImageCropper = document.getElementById('profile-image-cropper');
    if (profileImageCropper) {
        if (profileImageCroppie) {
            profileImageCroppie.destroy();
            profileImageCroppie = null;
        }
        profileImageCroppie = new Croppie(profileImageCropper, {
            viewport: { width: 320, height: 320, type: 'circle' },
            boundary: { width: 360, height: 360 },
            showZoomer: true,
            enableResize: false, // no fix aspect ratio
            mouseWheelZoom: false,
            enforceBoundary: true
        });

        if (imageUrl) {
            // wait a bit to fix weird behaviour
            setTimeout(function () {
                profileImageCroppie.bind({
                    url: imageUrl, zoom: true
                });
            }, 200);
        }
    }
}

// https://docs.microsoft.com/en-us/aspnet/core/blazor/images?view=aspnetcore-6.0
window.previewProfileImage = function (imageStream) {
    imageStream.arrayBuffer().then(function (arrayBuffer) {
        const blob = new Blob([arrayBuffer]);
        const imageUrl = URL.createObjectURL(blob);
        if (profileImageCroppie) {
            profileImageCroppie.bind({
                url: imageUrl
            });
        }
        // URL.revokeObjectURL(imageUrl);
    }).catch(function (ex) {
        console.error('PreviewProfileImage', ex);
    });
}

window.getProfileImageAsByteArray = async function () {
    if (profileImageCroppie) {
        const imageBlob = await profileImageCroppie.result({
            type: 'blob', format: 'jpeg', circle: false, quality: 0.8,
            size: { width: 320, height: 320 }
        });
        const arrayBuffer = await imageBlob.arrayBuffer();
        return new Uint8Array(arrayBuffer);
    }
    return "";
}

window.createProfileImageDropZone = function (dropZoneElement, inputFile) {

    const allowFileExts = ['image/jpeg', 'image/jpg', 'image/png'];

    function onDragHover(e) {
        e.preventDefault();
        dropZoneElement.classList.remove('error');
        dropZoneElement.classList.add('hover');
    }

    function onDragLeave(e) {
        e.preventDefault();
        dropZoneElement.classList.remove('hover');
    }

    function diplayProfileImage(file) {
        var fileReader = new FileReader();
        fileReader.addEventListener('load', (event) => {
            var newProfileImage = event.target.result;
            if (newProfileImage && profileImageCroppie) {
                profileImageCroppie.bind({
                    url: newProfileImage
                });
            }
        });
        fileReader.readAsDataURL(file);
    }

    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove('hover');
        dropZoneElement.classList.remove('error');
        
        let files = [];
        if (e.dataTransfer.items) {
            for (var i = 0; i < e.dataTransfer.items.length; i++) {
                // If dropped items aren't files, reject them
                if (e.dataTransfer.items[i].kind === 'file') {
                    files.push(e.dataTransfer.items[i].getAsFile())
                }
            }
        } else {
            files = e.dataTransfer.files;
        }

        if (files.length == 0) return;
        if (files.length > 1) {
            dropZoneElement.classList.add('error');
            dropZoneElement.querySelector('#error-message').innerHTML = "Please drop only one image";
            return;
        }
        // Get the first one
        const file = files[0];
        if (allowFileExts.indexOf(file.type.toLowerCase()) < 0) {
            dropZoneElement.classList.add('error');
            dropZoneElement.querySelector('#error-message').innerHTML = "Support an image only";
            return;
        }
        // Set the files property of the input element and raise the change event
        const dt = new DataTransfer();
        dt.items.add(file);
        inputFile.files = dt.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    // Register all events
    dropZoneElement.addEventListener('dragenter', onDragHover);
    dropZoneElement.addEventListener('dragover', onDragHover);
    dropZoneElement.addEventListener('dragleave', onDragLeave);
    dropZoneElement.addEventListener('drop', onDrop);

    // The returned object allows to unregister the events when the Blazor component is destroyed
    return {
        dispose: () => {
            if (!dropZoneElement) return;
            dropZoneElement.removeEventListener('dragenter', onDragHover);
            dropZoneElement.removeEventListener('dragover', onDragHover);
            dropZoneElement.removeEventListener('dragleave', onDragLeave);
            dropZoneElement.removeEventListener('drop', onDrop);
        }
    }
}