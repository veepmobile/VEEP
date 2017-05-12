import * as React from "react";
import {IFunction} from "../../models/Common"

interface IEditableSpanProps {
    id:string,
    text:string,
    containerClassName:string | null,
    changeNote:IFunction,
    is_in_editmode:boolean
};

interface IEditableSpanState {
    current_text:string,
    is_in_editmode:boolean
};

class EditableSpan extends React.Component<IEditableSpanProps, IEditableSpanState> {


    constructor(props:IEditableSpanProps){
        super(props);
        this.state={
            current_text:props.text,
            is_in_editmode:props.is_in_editmode
        };
    }

    editSpan(is_edit:boolean){
        this.setState({is_in_editmode:is_edit} as IEditableSpanState);
    }

    changeText(event: any){
        this.setState({current_text: event.target.value} as IEditableSpanState);
    }

    cancelChange(){
        this.setState({current_text: this.props.text} as IEditableSpanState);
    }

    submitChange(){
        this.props.changeNote(this.state.current_text);
    }


    public render(): JSX.Element {
        let clName=this.props.containerClassName===null ? "" : this.props.containerClassName;

        return (
            <div className={clName}>
                   {this.state.is_in_editmode ? 
                       <div> 
                        <div className="form-group">
                            <textarea className="form-control" autoFocus value={this.state.current_text} onBlur={this.editSpan.bind(this,false)} onChange={this.changeText.bind(this)}/>
                        </div>
                        <div className="form-group"><button className="btns darkblue" onMouseDown={this.submitChange.bind(this)} >Сохранить</button>
                        <button className="btns grey" onMouseDown={this.cancelChange.bind(this)}>Отмена</button></div></div>
                        :
                        <div className="spanContent" onClick={this.editSpan.bind(this,true)}>
                            {this.props.text}                                
                        </div>
                    }            
            </div>
        );
    }
}

export default EditableSpan;
