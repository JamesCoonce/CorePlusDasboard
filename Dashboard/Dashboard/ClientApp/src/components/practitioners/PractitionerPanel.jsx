import React, { Component } from 'react';
import DynamicTable from '@atlaskit/dynamic-table';
import ContentWrapper from '../ContentWrapper';
import PageTitle from '../PageTitle';
import {head, practRows} from '../../utils/util';

export default class PractitionerPanel extends Component {
    
  constructor(props){
    super(props);
    this.state = { rows: [] };
  }

  setStateAsync(state) {
    return new Promise((resolve) => {
      this.setState(state, resolve)
    });
  }

  async componentDidMount() {
    var rows = await practRows();
    console.log(rows);
    await this.setStateAsync({ rows: rows});
  }  

  render() {
    return (
      <ContentWrapper>
        <PageTitle>Practitioners</PageTitle>
        <DynamicTable
          caption={`List of Practitioners`}
          head={head}
          rows={this.state.rows}
          rowsPerPage={10}
          defaultPage={1}
          loadingSpinnerSize="large"
          isLoading={false}
          isFixedSize
          defaultSortKey="practitionerId"
          defaultSortOrder="ASC"
          onSort={() => console.log('onSort')}
          onSetPage={() => console.log('onSetPage')}
        />
      </ContentWrapper>
    );
  }
}