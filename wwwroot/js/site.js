// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let BacElem
function detailsOpen(id) {
    let headingElement = document.querySelector(`#${id}`)
if (BacElem) BacElem.style.display = 'none'
headingElement.style.display = 'block'
BacElem = headingElement
}
function detailsClose(id) {
    let headingElement = document.querySelector(`#${id}`)
headingElement.style.display = ''
}
