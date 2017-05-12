import * as React from "react";
import {IUserNote} from '../../models/IUserNote'
import {IDispatchId,IDispatchGet,IDispatchDelete} from '../../models/Common'
import EditableSpan from '../EditableSpan/EditableSpan'
import {GenerateGUID} from '../../lib/Utilites'


interface IDispatchAddNote{
    (id: string,content:string, issuer_id:string,can_use_cloud:boolean):void;
}

interface IDispatchUpdateNote{
    (content:string, issuer_id:string,note_id:string):void;
}

interface IDispatchNote{
    (note:IUserNote):void
}

interface IIssuerUserNotesProps {
    notes:IUserNote[],
    new_notes:IUserNote[],
    can_use_cloud:boolean,
    getNotes:IDispatchGet,
    saveNote:IDispatchAddNote,
    updadeNote:IDispatchUpdateNote,
    deleteNote:IDispatchDelete,
    addNewNote:IDispatchNote,
    deleteNewNote:IDispatchId,
    issuer_id:string
};

interface IIssuerUserNotesState {};

class IssuerUserNotes extends React.Component<IIssuerUserNotesProps, IIssuerUserNotesState> {


     componentDidMount() {
        this.props.getNotes(this.props.issuer_id);        
    }

    


    changeNoteContent(note_id:string,content:string){
        if(!content || content.trim()==""){
            return this.props.deleteNote(note_id);
        }
        this.props.updadeNote(content,this.props.issuer_id,note_id);
    }

    saveNewNote(note_id:string,content:string){
        if(!content || content.trim()==""){
            return this.props.deleteNewNote(note_id);
        }
        this.props.saveNote(note_id,content,this.props.issuer_id,this.props.can_use_cloud);
    }

    addNewNote(){
        let id=GenerateGUID();
        this.props.addNewNote({
            id:id,
            user_id:null,
            issuer_id:this.props.issuer_id
        }as IUserNote);        
    }




    public render(): JSX.Element {

        let notes=this.props.notes.map((note)=>{
            return(<tr key={note.id} className="note_item">
                    <td>
                        <span className="ddel" onClick={this.props.deleteNote.bind(this,note.id)}>
                        <span className="ddel_1">
                            <b>x</b>
                                Удалить
                            </span>
                        </span> 
                        <EditableSpan containerClassName="notePlaceholder" id={note.id} 
                        text={note.content} changeNote={this.changeNoteContent.bind(this,note.id)} is_in_editmode={false} />
                                                                  
                        <div className="link_block_info">
                            <span className="explain">Дата изменения:{note.update_date}</span>
                        </div>
                    </td>
                </tr>
            )
        });

        let new_notes=this.props.new_notes.map((note)=>{
            let id=String(note.id);
            return(<tr key={id} className="note_item">
                    <td>
                        <div className="cloud_warning">Не сохранено!</div>
                        <EditableSpan containerClassName="notePlaceholder" id={id} 
                        text={note.content} changeNote={this.saveNewNote.bind(this,id)} is_in_editmode={true} />                                           
                    </td>
                </tr>
            )
        });

        return (<div className="link_block" id="lb_2">                    
                    <h3 className="cloud_switcher_header">
                        <span className="icon-sticky-note-o"></span>Заметки
                        <span className="icon-angle-down cloud_switcher_ar"></span>
                    </h3>
                    <table className="extra_table" style={{border:0}} cellPadding="0" cellSpacing="0">
                        <tbody>
                            <tr>
                                <td>
                                    <div className="icon_block large" onClick={this.addNewNote.bind(this)}>
                                        <span className="icon-plus new ico"></span>
                                        <span>Добавить</span>                                       
                                    </div>                                
                                </td>
                            </tr>
                            {new_notes}
                             {notes}
                        </tbody> 
                    </table>                   
                </div>);
    }
}

export default IssuerUserNotes;
