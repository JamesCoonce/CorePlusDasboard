import React, { Component } from 'react';
import ContentWrapper from '../ContentWrapper';
import { getPractitioner } from  '../../utils/util';
import PageTitle from '../PageTitle';

export default class PractitionerDashboard extends Component {
  constructor(props){
    super(props);

    this.state = { practitioner: null,};
}

  setStateAsync(state) {
    return new Promise((resolve) => {
      this.setState(state, resolve)
    });
  }

  async componentDidMount() {
    await getPractitioner(this.props.match.params.id).then(practitioner => this.setStateAsync({practitioner}));
  } 

  render() {
    return (
      <ContentWrapper>
        <PageTitle>Settings</PageTitle>
        <h1>{this.props.match.params.id}</h1>
        <h2>{this.state.practitioner.name}</h2>
      </ContentWrapper>
    );
  }
}