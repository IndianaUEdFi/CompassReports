// Internet Explorer does not support startsWith or includes
// Typescript allows extension of interfaces through merging all with the same name together

interface String {
    includes(search: any, start?: number): boolean;
    startsWith(searchString: string, position?: number): boolean;
}

if (!(String.prototype).startsWith) {
    (String.prototype).startsWith = function (searchString: string, position?: number) {
        position = position || 0;
        return this.indexOf(searchString, position) === position;
    };
}

if (!(String.prototype).includes) {
    (String.prototype).includes = function (search: any, start?: number) {
        if (typeof start !== 'number') {
            start = 0;
        }

        if (start + search.length > this.length) {
            return false;
        } else {
            return this.indexOf(search, start) !== -1;
        }
    };
}