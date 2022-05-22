export function FileUploder_Init(dropZoneElement, inputFileSelector, inputFile) {
    const allowFileExts = ['image/jpeg', 'image/jpg', 'image/png', 'application/pdf',
        'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        'application/vnd.openxmlformats-officedocument.wordprocessingml.document'
    ];

    function onDragHover(e) {
        e.preventDefault();
        dropZoneElement.classList.remove('error');
        dropZoneElement.classList.add('hover');
    }

    function onDragLeave(e) {
        e.preventDefault();
        dropZoneElement.classList.remove('hover');
    }

    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove('hover');
        dropZoneElement.classList.remove('error');

        const dataTransfer = new DataTransfer();
        if (e.dataTransfer.items) {
            for (let i = 0; i < e.dataTransfer.items.length; i++) {
                const item = e.dataTransfer.items[i];
                // items includes sub directory
                if (item.kind === 'file' && allowFileExts.indexOf(item.type.toLowerCase()) > -1) {
                    dataTransfer.items.add(item.getAsFile())
                }
            }
        } else {
            for (let i = 0; i < e.dataTransfer.files.length; i++) {
                const file = e.dataTransfer.files[i];
                if (allowFileExts.indexOf(file.type.toLowerCase()) > -1) {
                    dataTransfer.items.add(file)
                }
            }
        }

        if (dataTransfer.files.length == 0) return;
        // Append files
        for (let i = 0; i < inputFile.files.length; i++) {
            const file = inputFile.files[i];
            dataTransfer.items.add(file);
        }

        // Set the files property of the input element and raise the change event
        inputFile.files = dataTransfer.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        const dataTransfer = new DataTransfer();
        
        for (let i = 0; i < e.clipboardData.files.length; i++) {
            const file = e.clipboardData.files[i];
            if (allowFileExts.indexOf(file.type.toLowerCase()) > -1) {
                dataTransfer.items.add(file)
            }
        }
        if (dataTransfer.files.length == 0) return;

        // Append files
        for (let i = 0; i < inputFile.files.length; i++) {
            const file = inputFile.files[i];
            dataTransfer.items.add(file);
        }

        inputFile.files = dataTransfer.files;
        const changeEvent = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(changeEvent);
    }

    function onFileChange(e) {
        const selectFiles = this.files;
        if (selectFiles.length == 0) return;
        
        const dataTransfer = new DataTransfer();
        // new selected files
        for (let i = 0; i < selectFiles.length; i++) {
            if (allowFileExts.indexOf(selectFiles[i].type.toLowerCase()) > -1) {
                dataTransfer.items.add(selectFiles[i]);
            }
        }

        if (dataTransfer.files.length == 0) return;
        // Append files
        for (let i = 0; i < inputFile.files.length; i++) {
            const file = inputFile.files[i];
            dataTransfer.items.add(file);
        }

        inputFile.files = dataTransfer.files;
        const changeEvent = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(changeEvent);
    }

    // Register all events
    dropZoneElement.addEventListener('dragenter', onDragHover);
    dropZoneElement.addEventListener('dragover', onDragHover);
    dropZoneElement.addEventListener('dragleave', onDragLeave);
    dropZoneElement.addEventListener('drop', onDrop);
    dropZoneElement.addEventListener('paste', onPaste);
    inputFileSelector.addEventListener('change', onFileChange);

    // The returned object allows to unregister the events when the Blazor component is destroyed
    return {
        dispose: () => {
            if (!dropZoneElement) return;
            dropZoneElement.removeEventListener('dragenter', onDragHover);
            dropZoneElement.removeEventListener('dragover', onDragHover);
            dropZoneElement.removeEventListener('dragleave', onDragLeave);
            dropZoneElement.removeEventListener('drop', onDrop);
            dropZoneElement.removeEventListener('paste', onPaste);
            inputFileSelector.removeEventListener('change', onFileChange);
        }
    }
}