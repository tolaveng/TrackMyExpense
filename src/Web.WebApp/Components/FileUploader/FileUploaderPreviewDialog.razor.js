export function fileUploaderImagePreview(imageStream, fileUploderPreviewImage, fileUploderPreviewDownload, fileName, isImage) {
    imageStream.arrayBuffer().then(function (arrayBuffer) {
        const blob = new Blob([arrayBuffer]);
        const objectUrl = URL.createObjectURL(blob);
        
        if (fileUploderPreviewImage && isImage) {
            fileUploderPreviewImage.src = objectUrl;
        }

        if (fileUploderPreviewDownload) {
            fileUploderPreviewDownload.href = objectUrl;
            fileUploderPreviewDownload.download = fileName;
        }
    }).catch(function (ex) {
        console.error('Preview Image', ex);
    });
}