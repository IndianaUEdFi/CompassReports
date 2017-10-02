module App.Services {
    export interface IColorGradient {
        getColors: (colorCount: number) => string[];
    }

    class ColorGradient {
        static $inject = ['$rootScope'];

        getColors(colorCount: number) {
            const color1Hash = this.$rootScope.primaryColor.color;
            const color2Hash = this.$rootScope.secondaryColor.color;

            const color1Hex = color1Hash.split('#')[1];
            const color2Hex = color2Hash.split('#')[1];

            const color1Red = parseInt(color1Hex.substr(0, 2), 16);
            const color1Green = parseInt(color1Hex.substr(2, 2), 16);
            const color1Blue = parseInt(color1Hex.substr(4, 2), 16);

            const color2Red = parseInt(color2Hex.substr(0, 2), 16);
            const color2Green = parseInt(color2Hex.substr(2, 2), 16);
            const color2Blue = parseInt(color2Hex.substr(4, 2), 16);

            const redDiff = (color1Red > color2Red) ? color1Red - color2Red : color2Red - color1Red;
            const greenDiff = (color1Green > color2Green) ? color1Green - color2Green : color2Green - color1Green;
            const blueDiff = (color1Blue > color2Blue) ? color1Blue - color2Blue : color2Blue - color1Blue;

            const redAvg = redDiff / (colorCount - 1);
            const greenAvg = greenDiff / (colorCount - 1);
            const blueAvg = blueDiff / (colorCount - 1);

            const colors = [color1Hash];
            let currentRed = color1Red;
            let currentGreen = color1Green;
            let currentBlue = color1Blue;

            for (let i = 0; i < (colorCount - 1); i++) {
                if (color1Red > color2Red) currentRed -= redAvg;
                else currentRed += redAvg;

                if (color1Green > color2Green) currentGreen -= greenAvg;
                else currentGreen += greenAvg;

                if (color1Blue > color2Blue) currentBlue -= blueAvg;
                else currentBlue += blueAvg;

                const color = '#' +
                    ((currentRed < 16) ? '0' : '') + Math.round(currentRed).toString(16) +
                    ((currentGreen < 16) ? '0' : '') +Math.round(currentGreen).toString(16) +
                    ((currentBlue < 16) ? '0' : '') +Math.round(currentBlue).toString(16);

                colors.push(color);
            }
            return colors;
        }

        constructor(private readonly $rootScope) {
        
    }
    }

    angular
        .module('app.services.color-gradient', [])
        .service('app.services.color-gradient', ColorGradient);
}