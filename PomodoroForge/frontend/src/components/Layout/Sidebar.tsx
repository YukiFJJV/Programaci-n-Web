export default function Sidebar() {
    return (
        <aside className="hidden md:flex md:w-64 md:flex-col bg-slate-950 border-r border-slate-800 justify-between p-5 shrink-0">
            <div>
                <div className="flex items-center gap-3 mb-8 px-2">
                    <div className="w-8 h-8 rounded-lg bg-indigo-600 flex items-center justify-center font-bold text-white shadow">
                            PF
                        </div>
                    <span className="text-xl font-bold tracking-tight text-white">PomodoroForge</span>
                </div>

                <nav className="space-y-2">
                    <a href="#" className="flex items-center gap-3 px-3 py-2.5 bg-indigo-600/15 text-indigo-400 rounded-lg font-medium text-sm border border-indigo-500/20">
                        <span>🌐</span> Catálogo Global
                    </a>
                    <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-500 rounded-lg font-medium text-sm cursor-not-allowed">
                        <span>📈</span> Estadisticas
                    </a>
                    <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-500 rounded-lg font-medium text-sm cursor-not-allowed">
                        <span>📊</span> Crear Pomodoro
                    </a>
                </nav>
            </div>

            <div className="p-3.5 bg-slate-900 rounded-xl border border-slate-800">
                <p className="text-xs text-slate-400 mb-1">Estado de Seguridad</p>
                <p className="text-sm font-semibold text-amber-400">👤 Modo Invitado</p>
            </div>
        </aside>
    );
}