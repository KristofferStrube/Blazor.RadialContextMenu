var currentContextMenuElements = [];
var currentObjRef;

export function Init(ObjRef) {
    currentObjRef = ObjRef;
    document.addEventListener("mouseup", function (e) {
        const menuItemsIntersection = e.path.filter(value => currentContextMenuElements.includes(value));
        if (menuItemsIntersection.length == 0) {
            currentObjRef.invokeMethodAsync("Close");
        }
    });
}

export function AddMenuElements(ContextMenuElements) {
    currentContextMenuElements = currentContextMenuElements.concat(ContextMenuElements)
}

export function RemoveMenuElements(ContextMenuElements) {
    currentContextMenuElements = currentContextMenuElements.path.filter(value => !ContextMenuElements.includes(value));
}