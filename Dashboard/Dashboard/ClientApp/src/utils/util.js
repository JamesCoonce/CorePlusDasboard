import React from 'react';
import Avatar from '@atlaskit/avatar';
import DropdownMenu, {
  DropdownItemGroup,
  DropdownItem,
} from '@atlaskit/dropdown-menu';
import styled from 'styled-components';

const baseUrl = 'https://localhost:44309/api/';

export const fetchPractitioners = async () => {
    var data = await fetch(`${baseUrl}/SampleData/practitioners`)
        .then(res => res.json())
    return data;
};

export const getPractitioner = async (id) => {
  var data = await fetch(`${baseUrl}/SampleData/practitioners/${id}`)
        .then(res => res.json())
    return data;
}

export const fetchAppointments = async (appointmentInfo) => {
  var data = await fetch(`${baseUrl}/SampleData/appointments`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            practitionerId: appointmentInfo.practitionerId,
            startDate: appointmentInfo.startDate,
            endDate: appointmentInfo.endDate
        })
    }).then(res => res.json())
    return data;
}
  
const NameWrapper = styled.span`
    display: flex;
    align-items: center;
`;
  
const AvatarWrapper = styled.div`
    margin-right: 8px;
`;


export const createPractionerHead = (withWidth) => {
    return {
      cells: [
        {
          key: 'name',
          content: 'Name',
          isSortable: true,
          width: withWidth ? 25 : undefined,
        },
        {
            key: 'practitionerId',
            content: 'ID',
            isSortable: true,
            width: withWidth ? 25 : undefined,
          },
        {
          key: 'appointments',
          content: 'Appointments',
          shouldTruncate: true,
          isSortable: true,
          width: withWidth ? 15 : undefined,
        },
      ],
    };
  };
  
  export const head = createPractionerHead(true);
  
  export const createRows = async data => {
    var data = await data;

    return await data.map((practs, index) => ({
        key: `row-${index}-${practs.practitionerId}`,
        cells: [
          {
            key: practs.name,
            content: (
              <NameWrapper>
                <AvatarWrapper>
                  <Avatar
                    name={practs.name}
                    size="medium"
                    src={`https://api.adorable.io/avatars/24/${encodeURIComponent(
                        practs.practitionerId,
                    )}.png`}
                  />
                </AvatarWrapper>
                <a href={`https://localhost:44309/practitioners/${practs.practitionerId}`}>{practs.name}</a>
              </NameWrapper>
            ),
          },
          {
            key: practs.practitionerId,
            content: practs.practitionerId,
          },
          {
            content: (
              <DropdownMenu trigger="More" triggerType="button">
                <DropdownItemGroup>
                  <DropdownItem>{practs.name}</DropdownItem>
                </DropdownItemGroup>
              </DropdownMenu>
            ),
          },
        ],
      }));
};

export const practRows = async () => {
    var rows = await createRows(fetchPractitioners());
    console.log(rows);
    return rows;
}

function createKey(input) {
    return input ? input.replace(/^(the|a|an)/, '').replace(/\s/g, '') : input;
  }

/*export const rows = Array.from(practData()).map((practs, index) => ({
    key: `row-${index}-${practs.practitionerId}`,
    cells: [
      {
        key: practs.name,
        content: (
          <NameWrapper>
            <AvatarWrapper>
              <Avatar
                name={practs.name}
                size="medium"
                src={`https://api.adorable.io/avatars/24/${encodeURIComponent(
                    practs.practitionerId,
                )}.png`}
              />
            </AvatarWrapper>
            <a href="https://atlassian.design">{practs.name}</a>
          </NameWrapper>
        ),
      },
      {
        key: practs.practitionerId,
        content: practs.practitionerId,
      },
      {
        content: (
          <DropdownMenu trigger="More" triggerType="button">
            <DropdownItemGroup>
              <DropdownItem>{practs.name}</DropdownItem>
            </DropdownItemGroup>
          </DropdownMenu>
        ),
      },
    ],
  }));
  */