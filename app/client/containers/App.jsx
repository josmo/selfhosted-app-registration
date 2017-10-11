import React from 'react';
import Iframe from 'react-iframe';

import Maintenance from './Maintenance.jsx';


export default class App extends React.Component {
  state = {
    authToken: 'testing'
  };
  onAuthenticate = (event) => {
    if (event.origin === "http://il1devapiapp01") {
      this.setState({ authToken: event.data })
    }
  };
  componentWillMount = () => {
    window.addEventListener("message", this.onAuthenticate, false);
  };



  render() {
    if (this.state.authToken === '') {
      return (
      <div><p>Authenticating...</p><Iframe url="http://il1devapiapp01/ClaimsService/jwtInfo.aspx"/></div>
      )
    }
    return (
        <Maintenance authToken={this.state.authToken}/>
    );
  }
}


