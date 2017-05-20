import { combineReducers } from 'redux';
import {user_files,upload_progress,file_size_limit} from './userFilesReducer';
import modalReducer from './modalReducer'
import {user_notes,new_user_notes} from './userNotesReducer';
import {user_addresses} from './userAddressReducer';
import {can_use_cloud} from './canUseCloudReducer'
import canUseCloudModalReducer from './canUseCloudModalReducer'

const rootReducer=combineReducers({
    files: user_files,
    upload_progress:upload_progress,
    modals:modalReducer,
    notes:user_notes,
    new_notes:new_user_notes,
    addresses:user_addresses,
    issuer_id:(state:string=null,action:any)=>(state),
    file_size_limit:file_size_limit,
    can_use_cloud:can_use_cloud,
    can_use_cloud_modals:canUseCloudModalReducer
});

export default rootReducer;