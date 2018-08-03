module App.Services {
    export interface IColorGradient {
        getHexColors: (colorCount: number) => string[];
        getRgbColors: (colorCount: number) => any[];
    }

    class ColorGradient implements IColorGradient {
        static $inject = ['$rootScope'];

        private getColor(colorCount: number, asRGB: boolean) {
            const color1Hash = (this.$rootScope.primaryColor) ? this.$rootScope.primaryColor.color : this.$rootScope.defaultPrimary.color ;
            const color2Hash = (this.$rootScope.tertiaryColor) ? this.$rootScope.tertiaryColor.color : this.$rootScope.defaultTertiary.color;
            const color3Hash = (this.$rootScope.secondaryColor) ? this.$rootScope.secondaryColor.color : this.$rootScope.defaultSecondary.color;

            const color1Hex = color1Hash.split('#')[1];
            const color2Hex = color2Hash.split('#')[1];
            const color3Hex = color3Hash.split('#')[1];

            const color1Red = parseInt(color1Hex.substr(0, 2), 16);
            const color1Green = parseInt(color1Hex.substr(2, 2), 16);
            const color1Blue = parseInt(color1Hex.substr(4, 2), 16);

            const color2Red = parseInt(color2Hex.substr(0, 2), 16);
            const color2Green = parseInt(color2Hex.substr(2, 2), 16);
            const color2Blue = parseInt(color2Hex.substr(4, 2), 16);

            const color3Red = parseInt(color3Hex.substr(0, 2), 16);
            const color3Green = parseInt(color3Hex.substr(2, 2), 16);
            const color3Blue = parseInt(color3Hex.substr(4, 2), 16);

            const redDiff = (color1Red > color2Red) ? color1Red - color2Red : color2Red - color1Red;
            const greenDiff = (color1Green > color2Green) ? color1Green - color2Green : color2Green - color1Green;
            const blueDiff = (color1Blue > color2Blue) ? color1Blue - color2Blue : color2Blue - color1Blue;

            const redDiff2 = (color2Red > color3Red) ? color2Red - color3Red : color3Red - color2Red;
            const greenDiff2 = (color2Green > color3Green) ? color2Green - color3Green : color3Green - color2Green;
            const blueDiff2 = (color2Blue > color3Blue) ? color2Blue - color3Blue : color3Blue - color2Blue;

            const halfColorCount = Math.round((colorCount / 2));

            const redAvg = redDiff / (halfColorCount);
            const greenAvg = greenDiff / (halfColorCount);
            const blueAvg = blueDiff / (halfColorCount);

            const redAvg2 = redDiff2 / ((colorCount - halfColorCount) - 1);
            const greenAvg2 = greenDiff2 / ((colorCount - halfColorCount) - 1);
            const blueAvg2 = blueDiff2 / ((colorCount - halfColorCount) - 1);

            let currentRed = color1Red;
            let currentGreen = color1Green;
            let currentBlue = color1Blue;

            const colors = [];
            if (asRGB) {
                colors.push({
                    r: Math.round(color1Red),
                    g: Math.round(color1Green),
                    b: Math.round(color1Blue)
                });
            }
            else { colors.push(color1Hash) }

            for (let i = 0; i < (colorCount - 1); i++) {

                if (i < halfColorCount) {
                    if (color1Red > color2Red) currentRed -= redAvg;
                    else currentRed += redAvg;

                    if (color1Green > color2Green) currentGreen -= greenAvg;
                    else currentGreen += greenAvg;

                    if (color1Blue > color2Blue) currentBlue -= blueAvg;
                    else currentBlue += blueAvg;
                } else {
                    if (color2Red > color3Red) currentRed -= redAvg2;
                    else currentRed += redAvg2;

                    if (color2Green > color3Green) currentGreen -= greenAvg2;
                    else currentGreen += greenAvg2;

                    if (color2Blue > color3Blue) currentBlue -= blueAvg2;
                    else currentBlue += blueAvg2;
                }

                if (!asRGB) {
                    const color = '#' +
                        ((currentRed < 16) ? '0' : '') + Math.round(currentRed).toString(16) +
                        ((currentGreen < 16) ? '0' : '') + Math.round(currentGreen).toString(16) +
                        ((currentBlue < 16) ? '0' : '') + Math.round(currentBlue).toString(16);

                    colors.push(color);
                } else {
                    colors.push({
                        r: Math.round(currentRed),
                        g: Math.round(currentGreen),
                        b: Math.round(currentBlue)
                    });
                }
            }
            return colors;
        }

        getHexColors(colorCount: number) { return this.getColor(colorCount, false); }
        getRgbColors(colorCount: number) { return this.getColor(colorCount, true); }

        constructor(private readonly $rootScope) {
        
    }
    }

    angular
        .module('app.services.color-gradient', [])
        .service('app.services.color-gradient', ColorGradient);
}