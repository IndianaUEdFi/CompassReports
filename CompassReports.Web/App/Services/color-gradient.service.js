var App;
(function (App) {
    var Services;
    (function (Services) {
        var ColorGradient = (function () {
            function ColorGradient() {
            }
            ColorGradient.prototype.getColors = function (colorCount) {
                var color1Hash = '#003E69';
                var color2Hash = '#FDCD0F';
                var color1Hex = color1Hash.split('#')[1];
                var color2Hex = color2Hash.split('#')[1];
                var color1Red = parseInt(color1Hex.substr(0, 2), 16);
                var color1Green = parseInt(color1Hex.substr(2, 2), 16);
                var color1Blue = parseInt(color1Hex.substr(4, 2), 16);
                var color2Red = parseInt(color2Hex.substr(0, 2), 16);
                var color2Green = parseInt(color2Hex.substr(2, 2), 16);
                var color2Blue = parseInt(color2Hex.substr(4, 2), 16);
                var redDiff = (color1Red > color2Red) ? color1Red - color2Red : color2Red - color1Red;
                var greenDiff = (color1Green > color2Green) ? color1Green - color2Green : color2Green - color1Green;
                var blueDiff = (color1Blue > color2Blue) ? color1Blue - color2Blue : color2Blue - color1Blue;
                var redAvg = redDiff / (colorCount - 1);
                var greenAvg = greenDiff / (colorCount - 1);
                var blueAvg = blueDiff / (colorCount - 1);
                var colors = [color1Hash];
                var currentRed = color1Red;
                var currentGreen = color1Green;
                var currentBlue = color1Blue;
                for (var i = 0; i < (colorCount - 1); i++) {
                    if (color1Red > color2Red)
                        currentRed -= redAvg;
                    else
                        currentRed += redAvg;
                    if (color1Green > color2Green)
                        currentGreen -= greenAvg;
                    else
                        currentGreen += greenAvg;
                    if (color1Blue > color2Blue)
                        currentBlue -= blueAvg;
                    else
                        currentBlue += blueAvg;
                    var color = '#' +
                        ((currentRed < 16) ? '0' : '') + Math.round(currentRed).toString(16) +
                        ((currentGreen < 16) ? '0' : '') + Math.round(currentGreen).toString(16) +
                        ((currentBlue < 16) ? '0' : '') + Math.round(currentBlue).toString(16);
                    colors.push(color);
                }
                return colors;
            };
            return ColorGradient;
        }());
        angular
            .module('app.services.color-gradient', [])
            .service('app.services.color-gradient', ColorGradient);
    })(Services = App.Services || (App.Services = {}));
})(App || (App = {}));
//# sourceMappingURL=color-gradient.service.js.map