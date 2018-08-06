import { REQUEST, SUCCESS, FAILURE } from '../utils/action-type.util';
import { fetchPractitioners, fetchAppointments, getPractitioner} from  '../utils/util.js';
export const ACTION_TYPES = {
    FETCH_PRACTITIONER: 'FETCH_PRACTITIONERS',
    FETCH_APPOINTMENTS:  'FETCH_APPOINTMENTS',
    GET_PRACTITIONER: 'GET_PRACTITIONER',
};

const initialState = {
    loading: false,
    appointments: [],
    practitioners: [],
    currentPractitioner: null,
    updateSuccess: false,
    errorMessage: action.payload
};

export const loadPractitioners = async (dispatch) => dispatch({
    type: ACTION_TYPES.FETCH_PRACTITIONERS, 
    payload: fetchPractitioners()
});

export const loadPractitioner = async (dispatch, id) => dispatch({
    type: ACTION_TYPES.GET_PRACTITIONER, 
    payload: getPractitioner(id)
});

export const loadAppointments = async (dispatch, appointmentInfo) => dispatch({
    type: ACTION_TYPES.FETCH_APPOINTMENTS, 
    payload: fetchAppointments(appointmentInfo)
});

export default (state = initialState, action) => {
    switch (action.type) {
        case REQUEST(ACTION_TYPES.FETCH_PRACTITIONERS):
        case REQUEST(ACTION_TYPES.FETCH_APPOINTMENTS):
        case REQUEST(ACTION_TYPES.GET_APPOINTMENT):
            return {
                ...state,
                loading: true
            };
        case FAILURE(ACTION_TYPES.FETCH_PRACTITIONERS):
        case FAILURE(ACTION_TYPES.FETCH_APPOINTMENTS):
        case FAILURE(ACTION_TYPES.GET_APPOINTMENT):
        return {
            ...state,
            loading: false,
            updating: false,
            updateSuccess: false,
            errorMessage: action.payload
          };
        case SUCCESS(ACTION_TYPES.FETCH_PRACTITIONERS):
          return {
            ...state,
            loading: false,
            practitioners: action.payload.data
          };
        case SUCCESS(ACTION_TYPES.FETCH_APPOINTMENTS):
          return {
            ...state,
            loading: false,
            appointments: action.payload.data
          };
        case SUCCESS(ACTION_TYPES.GET_PRACTITIONER):
          return {
            ...state,
            loading: false,
            currentPractitioner: action.payload.data
          };
    }
}