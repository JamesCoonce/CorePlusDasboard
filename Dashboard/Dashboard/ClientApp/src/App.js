﻿import React, { Component } from 'react';
import Flag, { FlagGroup } from '@atlaskit/flag';
import Modal from '@atlaskit/modal-dialog';
import Page from '@atlaskit/page';
import '@atlaskit/css-reset';

import StarterNavigation from './components/StarterNavigation';

export default class App extends Component {
  state = {
    flags: [],
    isModalOpen: false,
  };

  showModal = () => {
    this.setState({ isModalOpen: true });
  }

  hideModal = () => {
    this.setState({ isModalOpen: false });
  }

  addFlag = () => {
    this.setState({ flags: [{ id: Date.now() }].concat(this.state.flags) });
  }

  onFlagDismissed = (dismissedFlagId) => {
    this.setState({
      flags: this.state.flags.filter(flag => flag.id !== dismissedFlagId),
    })
  }

  render() {
    return (
      <div>
        <Page
          navigationWidth={this.props.navOpenState.width}
          navigation={<StarterNavigation />}
        >
          {this.props.children}
        </Page>
        <div>
          <FlagGroup onDismissed={this.onFlagDismissed}>
            {
              this.state.flags.map(flag => (
                <Flag
                  id={flag.id}
                  key={flag.id}
                  title="Flag Title"
                  description="Flag description"
                />
              ))
            }
          </FlagGroup>
          {
            this.state.isModalOpen && (
              <Modal
                heading="Candy bar"
                actions={[{ text: 'Exit candy bar', onClick: this.hideModal }]}
                onClose={this.hideModal}
              >
                <p style={{ textAlign: 'center' }}>
                  <img src="http://i.giphy.com/yidUztgRB2w2gtDwL6.gif" alt="Moar cupcakes" />
                </p>
              </Modal>
            )
          }
        </div>
      </div>
    );
  }
}