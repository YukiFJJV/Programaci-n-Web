export type PomodoroStatus =
    'Pending' | 'Completed' | 'Interrupted' | 'InProgress';

export interface PomodoroSessionDto {
    id: string;
    globalPomodoroId: string;
    name: string;
    startDate: string | Date;
    finishDate: string | Date;
    status: PomodoroStatus;
    cycles: number;
    durationMinutes: number;
}