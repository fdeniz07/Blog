//Site.js bizlerin ortak javascript kodlarinin bulundugu alandir.

function convertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
}

function convertToShortDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}

//Coklu ref degeri cözümü baslangic noktasi

// coklu dizi elemanlari sonucunda ilk id degerinden sonra diger id ler ref degeri dönerse bu kismi yapistiriyoruz.

function getJsonNetObject(obj, parentObj) {
    // check if obj has $id key.
    var objId = obj["$id"];
    if (typeof (objId) !== "undefined" && objId != null) {
        // $id key exists, so you have the actual object... return it
        return obj;
    }
    // $id did not exist, so check if $ref key exists.
    objId = obj["$ref"];
    if (typeof (objId) !== "undefined" && objId != null) {
        // $ref exists, we need to get the actual object by searching the parent object for $id
        return getJsonNetObjectById(parentObj, objId);
    }
    // $id and $ref did not exist... return null
    return null;
}

// function to return a JSON object by $id
// parentObj: the top level object containing all child objects as serialized by JSON.NET.
// id: the $id value of interest
function getJsonNetObjectById(parentObj, id) {
    // check if $id key exists.
    var objId = parentObj["$id"];
    if (typeof (objId) !== "undefined" && objId != null && objId == id) {
        // $id key exists, and the id matches the id of interest, so you have the object... return it
        return parentObj;
    }
    for (var i in parentObj) {
        if (typeof (parentObj[i]) == "object" && parentObj[i] != null) {
            //going one step down in the object tree
            var result = getJsonNetObjectById(parentObj[i], id);
            if (result != null) {
                // return found object
                return result;
            }
        }
    }
    return null;
}

//Coklu ref degeri cözümü bitis noktasi