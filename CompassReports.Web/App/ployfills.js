// Internet Explorer does not support startsWith or includes
// Typescript allows extension of interfaces through merging all with the same name together
if (!(String.prototype).startsWith) {
    (String.prototype).startsWith = function (searchString, position) {
        position = position || 0;
        return this.indexOf(searchString, position) === position;
    };
}
if (!(String.prototype).includes) {
    (String.prototype).includes = function (search, start) {
        if (typeof start !== 'number') {
            start = 0;
        }
        if (start + search.length > this.length) {
            return false;
        }
        else {
            return this.indexOf(search, start) !== -1;
        }
    };
}
//# sourceMappingURL=ployfills.js.map