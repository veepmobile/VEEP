import * as React from "react";
import { connect } from 'react-redux';



import IssuerUserNotes  from "./IssuerUserNotesView";
import {IIssuerState} from "../../models/IIssuerState";
import {IssuerNoteActionCreators} from '../../actions/IssuerNoteActionCreators'
import {IUserNote} from '../../models/IUserNote'
import {ModalActionCreators} from '../../actions/ModalActionCreators'
import {AccessCloudActionCreators} from '../../actions/AccessCloudActionCreators'



const mapStateToProps=(state:IIssuerState)=>(
    {
        notes:state.notes,
        new_notes:state.new_notes,
        issuer_id:state.issuer_id,
        can_use_cloud:state.can_use_cloud
    }
);

const mapDispatchToProps=(dispatch:any)=>(
    {
            getNotes:(issuer_id: string )=>(dispatch(IssuerNoteActionCreators.GetNotes(issuer_id))),
            saveNote:(note_id:string,content:string, issuer_id:string,can_use_cloud:boolean)=>{
                if(can_use_cloud){
                    dispatch(IssuerNoteActionCreators.AddNote(note_id,content,issuer_id));
                }else{
                    dispatch(AccessCloudActionCreators.showModal(IssuerNoteActionCreators.AddNote(note_id,content,issuer_id)));
                }                
            },
            updadeNote:(content:string, issuer_id:string, note_id:string)=>(dispatch(IssuerNoteActionCreators.UpdateNote(content,issuer_id,note_id))),
            deleteNote:(note_id:string)=>{
                dispatch(ModalActionCreators.showConfirmModal("Вы действительно хотите удалить заметку?",IssuerNoteActionCreators.DeleteNote(note_id)))                
            },
            addNewNote:(note:IUserNote)=>(dispatch(IssuerNoteActionCreators.AddNewNote(note))),
            deleteNewNote:(note_id:string)=>(dispatch(IssuerNoteActionCreators.DeleteNewNote(note_id)))
    }
);



export default connect(mapStateToProps,mapDispatchToProps)(IssuerUserNotes)