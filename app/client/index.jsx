import React from 'react';
import ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import App from './containers/App.jsx';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import baseTheme from 'material-ui/styles/baseThemes/lightBaseTheme';
import * as Colors from 'material-ui/styles/colors';
import { fade } from 'material-ui/utils/colorManipulator'

const getTheme = () => {
  let overwrites = {
    "palette": {
      "primary1Color": "#51284F",
      "primary2Color": "#51284F",
      "accent1Color": "#448aff"
    }
  };
  return getMuiTheme(baseTheme, overwrites);
};

const render = Component => {
  ReactDOM.render(
    <AppContainer>
      <MuiThemeProvider muiTheme={getTheme()}>
        <Component/>
      </MuiThemeProvider>
    </AppContainer>
  , document.getElementById('root'));
};

render(App);

if(module.hot) {
  module.hot.accept('./containers/App.jsx', () => render(App))
}

