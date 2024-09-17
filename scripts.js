const dropArea = document.getElementById('drop-area');
const droppedContent = document.getElementById('dropped-content');
const fileList = document.getElementById('file-list');

dropArea.addEventListener('dragenter', preventDefaults, false); //Este evento se dispara cuando un elemento se está arrastrando y entra en el área de caída
dropArea.addEventListener('dragover', preventDefaults, false);  //Se activa continuamente mientras el elemento arrastrado está sobre el área de caída.
dropArea.addEventListener('dragleave', handleDragLeave, false); //Este evento ocurre cuando el elemento arrastrado sale del área de caída.
dropArea.addEventListener('drop', handleDrop, false);

dropArea.addEventListener('dragenter', highlight, false);
dropArea.addEventListener('dragover', highlight, false);
dropArea.addEventListener('dragleave', unhighlight, false);
dropArea.addEventListener('drop', unhighlight, false);
dropArea.addEventListener('click', openFileDialog, false);

function preventDefaults(event) {
  event.preventDefault();
  event.stopPropagation();
}

function highlight() {
  dropArea.classList.add('highlight');
  dropArea.innerHTML = `
  <div class="drop-icon">
    <i class="fa-light fa-file-upload"></i>
  </div>
  <div class="drop-text">Drop files</div>
  `; // Add this line
}

function unhighlight() {
  dropArea.classList.remove('highlight');
  dropArea.innerHTML = `
  <div class="drop-icon">
    <i class="fa-light fa-file-upload"></i>
  </div>
  <div class="drop-text">Drag and drop files here</div>
  `;
}

function handleDragLeave(event) {
  if (event.relatedTarget !== null) {
    return;
  }
  unhighlight();
}

function handleDrop(event) {
  event.preventDefault();
  const file = event.dataTransfer.files[0];
  const reader = new FileReader();

  reader.readAsText(file);
  reader.onload = function () {
    droppedContent.value = reader.result;
  };

  unhighlight();
}

function openFileDialog(event) {
  const fileInput = document.createElement('input');
  fileInput.type = 'file';
  fileInput.accept = 'application/json'; //solo aceptara JSON
  fileInput.multiple = true;

  fileInput.addEventListener('change', handleFileSelect, false);

  fileInput.click();
}

function handleFileSelect(event) {
  const files = Array.from(event.target.files); //convierte a array

  files.forEach(file => { //recorrer el array para leer cada uno de los json
    const reader = new FileReader();
    reader.readAsText(file);
    reader.onload = function() {
        droppedContent.value = reader.result;
        displayFile(file, reader.result);
      };
  });
}


function displayFile(file, content) {
    const listItem = document.createElement('li');
    listItem.className = 'item';
    
    const fileDetails = document.createElement('span');
    fileDetails.className = 'file-details';
    fileDetails.textContent = `${file.name} (${(file.size / 1024).toFixed(2)} KB)`;
  
    const deleteIcon = document.createElement('i');
    deleteIcon.className = 'fa-solid fa-trash-can';
    deleteIcon.style.cursor ='pointer';
    deleteIcon.addEventListener('click', () => {
      listItem.remove();
    });

    const fragment = document.createDocumentFragment();
    fragment.appendChild(fileDetails);
    fragment.appendChild(deleteIcon);

  // Añadir el fragmento al listItem
  listItem.appendChild(fragment);
    
    fileList.appendChild(listItem);
  }

const chars = document.getElementById('chars');

droppedContent.addEventListener('input', () => {
  chars.innerHTML = `${droppedContent.value.length}/5000`;
});

const outerDot = document.getElementById('outer-dot');
const dot = document.getElementById('dot');
let isSpellcheck = true;

outerDot.addEventListener('click', () => {
  isSpellcheck = !isSpellcheck;
  droppedContent.focus();
  
  if (!isSpellcheck) {
    dot.style.transform = 'none';
    outerDot.style.backgroundColor = '#444';
    droppedContent.spellcheck = false;
  } else {
    dot.removeAttribute('style');
    outerDot.removeAttribute('style');
    droppedContent.spellcheck = true;
  }
});