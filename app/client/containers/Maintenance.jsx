import React from 'react';
import { AppBar, IconButton } from 'material-ui';
import ActionHome from 'material-ui/svg-icons/action/home';
import ApplicationsScreen from './ApplicationsScreen.jsx';

import {
  HashRouter as Router,
  Route,
  Link,
  Switch
} from 'react-router-dom'


const Maintenance = () => (
  <Router>
    <div>
      <AppBar
        title="Gugg Applications List" style={{ position: 'fixed' }}
        iconElementLeft={<IconButton containerElement={<Link to='/'/>}><ActionHome/></IconButton>}
      />
      <Switch>
        <Route exact path="/" component={ApplicationsScreen}/>
      </Switch>
    </div>
  </Router>


);
export default Maintenance;

