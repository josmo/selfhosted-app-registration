import React from 'react';
import Paper from 'material-ui/Paper';
import { List, CircularProgress, ListItem, Avatar, Card, CardText, CardHeader,CardActions, FlatButton } from 'material-ui';
import Web from 'material-ui/svg-icons/av/web';
import {
  Link
} from 'react-router-dom'

class ApplicationsScreen extends React.Component {
  state = {
    applications: [],
  };



    rowsForApplications(application) {
        return (
              <ListItem key={application.applicationID}
                disabled={true}
              >
                <Card>
                  <CardHeader
                    title={application.applicationName}
                    subtitle={application.description}
                    avatar={<Avatar icon={<Web/>}/>}
                  />
                  <CardActions>
                    <a href={application.applicationURL}>
                      <FlatButton label="START"  />
                    </a>
                  </CardActions>
                </Card>
              </ListItem>
        )
    };
  componentDidMount() {
    console.log('mount');
    fetch('http://localhost:8888/v1/applications', {
      method: 'GET',
      headers: {
        'Accept': 'application/json',
        'Authorization': `Bearer: value`,
      },
    }).then(response => response.json())
      .then(applications => {
        console.log(applications)
        this.setState({applications});
      })
  }
  render () {
        return (
            <div>
                <Paper zDepth={4} style={{paddingTop:60}}>
                  <List style={{width:600}}>
                    {this.state.applications.map(this.rowsForApplications)}
                  </List>
                </Paper>
            </div>
        )
    }
};

export { ApplicationsScreen as default };
