import { BehaviorSubject, Observable, Operator, Subject } from 'rxjs'

export class ReadOnlySubject<T> extends BehaviorSubject<T>{

    static from<S>(subject: BehaviorSubject<S>): ReadOnlySubject<S> {
        return subject as ReadOnlySubject<S>
    }

    next(value?: T): void {
        throw new Error('The \'next\' method was used on a Read-only Subject!')
    }

}
