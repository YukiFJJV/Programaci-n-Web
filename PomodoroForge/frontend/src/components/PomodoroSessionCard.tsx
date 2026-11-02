import type { PomodoroSessionDto } from "../types/PomodoroSessionDto";

interface PomodoroSessionCardProps {
    session: PomodoroSessionDto;
    onClick?: (id: string) => void;
}

export default function PomodoroSessionCard({
  session,
  onClick,
}: PomodoroSessionCardProps) {
  const isCompleted = session.status === 'Completed';

  return (
    <div
      onClick={() => onClick?.(session.id)}
      className={`bg-card border border-border rounded-xl p-4 flex flex-col gap-3 transition-all hover:border-primary/50 cursor-pointer`}
    >
      {/* Cabecera: Fecha/Hora y Estado */}
      <div className="flex items-center justify-between text-xs">
        <span className="text-text-muted font-medium">
          {/* Aquí puedes formatear la fecha según prefieras, ej: "Today, 14:30" */}
          {String(session.startDate)}
        </span>

        <span className={`inline-flex items-center gap-1.5 px-2.5 py-1 rounded-full text-xs font-semibold ${
          isCompleted
            ? 'bg-success/10 text-success border border-success/20'
            : 'bg-danger/10 text-danger border border-danger/20'
        }`}>
          <span className={`w-1.5 h-1.5 rounded-full ${isCompleted ? 'bg-success' : 'bg-danger'}`} />
          {session.status}
        </span>
      </div>

      {/* Título principal */}
      <h3 className="text-text-main font-semibold text-base truncate">
        {session.name}
      </h3>

      {/* Pie de tarjeta: Categoría, Ciclos y Duración */}
      <div className="flex items-center justify-between mt-1">
        <div className="flex items-center gap-2">
            <span className="px-2.5 py-0.5 rounded-md text-xs font-medium bg-badge-bg text-badge-text">
            </span>
          <span className="text-text-muted text-xs">
            {session.cycles} {session.cycles === 1 ? 'cycle' : 'cycles'}
          </span>
        </div>

        <span className="text-text-main font-bold text-sm">
          {session.durationMinutes}m
        </span>
      </div>
    </div>
  );
};